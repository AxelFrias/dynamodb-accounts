aws dynamodb query \
    --table-name am-accounts-test \
    --key-condition-expression "partition_key = :partition_key and search_resource = :search_resource" \
    --expression-attribute-values file://expression.json \
    --return-consumed-capacity TOTAL \
> result.json