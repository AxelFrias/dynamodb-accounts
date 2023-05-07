import sys
import time
from awsglue.transforms import *
from awsglue.utils import getResolvedOptions
from pyspark.context import SparkContext
from awsglue.context import GlueContext
from awsglue.job import Job
from awsglue.dynamicframe import DynamicFrame

## @params: [JOB_NAME]
args = getResolvedOptions(sys.argv, ['JOB_NAME'])
sc = SparkContext()
glueContext = GlueContext(sc)
spark = glueContext.spark_session
job = Job(glueContext)
job.init(args['JOB_NAME'], args)

accounts_source_table = "am-accounts-for-migration-tests"
subaccounts_source_table = "am-subaccounts-for-migration-tests"
target_table = "am-accounts-test"

## Accounts DynamoDbSource
start = time.time()
dynamo_accounts_source = glueContext.create_dynamic_frame.from_options(
    "dynamodb",
    connection_options={
        "dynamodb.input.tableName": accounts_source_table,
        "dynamodb.throughput.read.percent": "1.0"
    },
    transformation_ctx = "dynamo_accounts_source"
)
end = time.time()
print(f"DynamoDb account exportation time is: {(end - start):.3f} ms.")

# Subaccounts DynamoDbSource
# start = time.time()
# dynamo_subaccounts_source = glueContext.create_dynamic_frame.from_options(
#     "dynamodb",
#     connection_options = {
#         "dynamodb.input.tableName": subaccounts_source_table,
#         "dynamodb.throughput.read.percent": "1.0"
#     },
#     transformation_ctx = "dynamo_subaccounts_source"
# )
# end = time.time()
# print(f"DynamoDb subaccount exportation time is: {(end - start):.3f} ms.")

# TODO add missing filters
# filters to be replaced
filters = [
    "{test}#TEST1",
    "{test2}",
    "{test3}#TEST2#TEST3"
]

# TODO replace filter name by its value
# Add N records based on filters
def multiply_n_records(records):
    for count, record in enumerate(records):
        for filter in filters:
            row = record.asDict()
            row['partition_key'] = f'{count}'
            row['search_resource'] = f"ACCOUNT#{filter}"
            yield row
multiply_n_data_frame = DynamicFrame.fromDF(
    spark.createDataFrame(
        multiply_n_records(dynamo_accounts_source.toDF().collect())
    ), 
    glueContext, 
    "multiply_n_data_frame"
)

# TODO for testing, remove when validate information
print("Inspect the multiply_n_data_frame")
multiply_n_data_frame.toDF().show()

# TODO Before merging, map columns as expected (for example, Glue casts holder as string, but is a map)

# Unir los registros de ambas tablas
# joined_tables = accounts_mapping.union(subaccounts_mapping)
# print("Joined")
# joined_tables.printSchema()

# TODO Change multiply_n_data_frame by joined tables
## Escribir los datos en la tabla de destino
start = time.time()
transformation_result = glueContext.write_dynamic_frame.from_options(
    frame = multiply_n_data_frame, 
    connection_type = "dynamodb", 
    connection_options = {
        "dynamodb.output.tableName": target_table, 
        "dynamodb.throughput.write.percent": "1.0"}, 
    transformation_ctx = "transformation_result"
)
end = time.time()
print(f"DynamoDb importation time is: {(end - start):.3f} ms.")

job.commit()