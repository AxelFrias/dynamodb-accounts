#!/bin/bash
aws dynamodb query \
    --table-name am-accounts-test \
    --index-name filters-GSI \
    --key-condition-expression "org_corp = :org_corp AND begins_with(filters, :filters)" \
    --filter-expression "#e = :ending_in" \
    --expression-attribute-names '{"#e": "ending_in"}' \
    --expression-attribute-values file://expression.json \
    --return-consumed-capacity TOTAL \
> result10000.json