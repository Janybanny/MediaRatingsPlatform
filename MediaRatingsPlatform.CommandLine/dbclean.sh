#!/bin/sh

docker stop mrpdb 
docker rm mrpdb 
echo "Old Container Removed"
sleep 2

docker run -d --name mrpdb -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=postgres -p 5432:5432 postgres
echo "New Container Started"
sleep 2
docker exec -i mrpdb createdb -U postgres mrpdb
echo "New Clean Database created"
