FROM mcr.microsoft.com/mssql/server:2017-latest
RUN apt-get update && apt-get install -y dos2unix

RUN mkdir -p /mssql
WORKDIR /msqql

COPY . /mssql
COPY ./migrations/ /mssql

RUN dos2unix /mssql/entrypoint.sh
RUN dos2unix /mssql/mssql-init.sh
RUN dos2unix /mssql/01-init-database.sql
RUN chmod +x /mssql/entrypoint.sh
RUN chmod +x /mssql/mssql-init.sh
RUN chmod +x /mssql/01-init-database.sql

EXPOSE 1433
ENTRYPOINT ["/mssql/entrypoint.sh"]