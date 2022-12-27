# Composed sort key type
aws dynamodb put-item \
    --table-name am-accounts-test  \
    --item \
        '{
            "partition_key": {"S": "c68f388b-21bc-4491-bd83-c40d125c1621#13afc650-1d5c-4f71-a164-55209b11d299#88c2dcd5-cdf3-46da-8376-18349b179341"}, 
            "resource": {"S": "account"}, 
            "corporation_id": {"S": "13afc650-1d5c-4f71-a164-55209b11d299"}, 
            "created_at": {"S": "2021-03-22T18:59:43.279Z"},
            "currency_code": {"S": "MXN"},
            "ending_in": {"S": "83027Z"},
            "external_id": {"S": "9608302"},
            "friendly_name": {"S": "2021-03-22T18:59:43.279Z"},
            "id": {"S": "88c2dcd5-cdf3-46da-8376-18349b179341"},
            "idempotency_key": {"S": "48152339db2a310feb4e5c9b6343c8ae22c859838dcdab5703b005b19e789710"},
            "is_sand_box": {"S": "1"},
            "org_id": {"S": "c68f388b-21bc-4491-bd83-c40d125c1621"},
            "ownership_Type": {"S": "OWNER"},
            "purpose_types": {"SS": ["DEBITING"]},
            "status": {"SS": ["DEBITING"]},
            "type": {"S": "CLABE"},
            "vault_id": {"S": "375ea9f3-a751-40b1-b43f-3cd6b28c4935"},
            "lorem_contract_id": {"S": "375ea9f3-a751-40b1-b43f-3cd6b28c4935"},
            "number": {"N": "3123132"},
            "filters": {"S": "OWNER"},
            "holder": {"M": {"type":{"S":"PERSONAL"},"identifiers":{"L":[{"M":{"value":{"S":"EEBX6811144Z3"},"identifier_type":{"S":"RFC"}}},{"M":{"value":{"S":"EEBX681114LDFLQL09"},"identifier_type":{"S":"CURP"}}}]},"full_name":{"S":"XOLEPE ELEBZOP BOQEELLE"}}},
            "financial_institution": {"M": {"short_name":{"S":"GBM"},"code":{"S":"601"},"legal_name":{"S":"GBM Grupo Bursátil Mexicano, S.A. de C.V. Casa de Bolsa"},"sector":{"S":"BROKERAGE_HOUSE"},"spei_party_code":{"S":"90601"}}}
        }' \
    --return-consumed-capacity TOTAL
