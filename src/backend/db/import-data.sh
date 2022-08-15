#!/bin/bash

sleep 20s
for i in {1..50};
do

    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Test@12345 -d master -i data.sql
    if [ $? -eq 0 ]
    then
        #echo "Import data"
        #/opt/mssql-tools/bin/bcp WeatherForecasts in ./data.csv -S localhost -U sa -P Test@12345 -d TestDb -c -t ','
        echo "Schema and data imported"
        break
    else
        echo "not ready yet..."
        sleep 1
    fi
done

# # wait for the SQL Server to come up
# sleep 30s
# #run the setup script to create the DB and the schema in the DB
# /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Test@12345" -i data.sql

