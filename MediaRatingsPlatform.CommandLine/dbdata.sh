#!/bin/sh

podman stop mrpdb 
podman rm mrpdb 
echo "Old Container Removed"
sleep 2

podman volume create mrpdb-data

podman run -d --name mrpdb -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=postgres -p 5432:5432 -v mrpdb-data:/var/lib/postgresql/data postgres

echo "New Container Started"
sleep 2

podman exec -i mrpdb createdb -U postgres mrpdb
echo "Database with volume started"
