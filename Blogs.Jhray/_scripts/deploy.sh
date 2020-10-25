#!bin/bash

echo "Running deploy script on master branch"

dotnet publish Blogs.Jhray/Blogs.Jhray.csproj
cd /home/travis/build/PurplePenguin4102/Blogs.Jhray/Blogs.Jhray/bin/Debug/netcoreapp3.1/publish/
echo "Initiating Git"
git init
git remote add deploy "deploy@jhray.com:/var/blogs.jhray"
git config user.name "Travis CI"
git config user.email "joseph.h.ray@gmail.com"
git add .
git commit -m "Deploy"
echo "Running git push"
git push --force deploy master
