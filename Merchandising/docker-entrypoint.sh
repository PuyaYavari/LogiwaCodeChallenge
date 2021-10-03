apt-get update -y && apt-get install -y openssl zip unzip git && apt-get install -y sqlite3 libsqlite3-dev
sqlite3 Merchandising.db < init.sql

dotnet Merchandising.dll