#!/bin/sh

docker stop mrpdb 2>/dev/null || true
docker rm mrpdb 2>/dev/null || true
echo "Old Container Removed (if any exist)"
sleep 2

if ! docker volume inspect mrpdb-data > /dev/null 2>&1; then
  docker volume create mrpdb-data
fi

docker run -d \
  --name mrpdb \
  -e POSTGRES_USER=postgres \
  -e POSTGRES_PASSWORD=postgres \
  -p 5432:5432 \
  -v mrpdb-data:/var/lib/postgresql/data \
  postgres:latest

echo "New Container Started"
sleep 5

# Wait for PostgreSQL to be ready before creating database
docker exec mrpdb pg_isready -U postgres
while [ $? -ne 0 ]; do
  echo "Waiting for PostgreSQL to start..."
  sleep 2
  docker exec mrpdb pg_isready -U postgres
done

docker exec -i mrpdb createdb -U postgres mrpdb
echo "Database with volume started"
