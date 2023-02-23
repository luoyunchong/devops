#!/bin/bash
#docker 镜像tag/容器名字 这里都命名为这个
SERVER_NAME=devops_web
SERVER_PORT=8087
SERVER_Dockerfile=DevOps.Web/Dockerfile
#获取容器id
CID=$(docker ps | grep "${SERVER_NAME}" | awk '{print $1}')
#获取镜像id
IID=$(docker images | grep "${SERVER_NAME}" | awk '{print $3}')

# 构建docker镜像
function build(){
#   if [ -n "$IID" ]; then
        echo "存在${SERVER_NAME}镜像，IID=$IID"
        echo "停止容器docker stop  $CID"
        docker stop  $CID
        echo "删除容器 docker rm $CID"
        docker rm $CID
        echo "删除镜像 docker rmi ${SERVER_NAME}"
        docker rmi ${SERVER_NAME}
        echo "构建镜像 docker build -f ${SERVER_Dockerfile} -t "${SERVER_NAME}"  ."
        docker build -f ${SERVER_Dockerfile} -t "${SERVER_NAME}":latest  .
#    else
#        echo "构建镜像 docker build -f ${SERVER_Dockerfile} -t "${SERVER_NAME}"  ."
#        docker build  -f ${SERVER_Dockerfile}  -t "${SERVER_NAME}":latest 
#    fi
}

# 运行docker容器
function run(){
    build
    echo "docker run -d --restart=always  --name=${SERVER_NAME} -p ${SERVER_PORT}:80   ${SERVER_NAME}"
    docker run -d --restart=always --name=${SERVER_NAME} -p ${SERVER_PORT}:80 \
    -v /var/www/${SERVER_NAME}/appsettings.Production.json/:/app/appsettings.Production.json:rw \
    -v /var/www/${SERVER_NAME}/${SERVER_NAME}.db/:/app/${SERVER_NAME}.db:rw \
    ${SERVER_NAME}
    echo "删除中间镜像 docker image prune -f"
    docker image prune -f
    echo "${SERVER_NAME}容器创建完成"
}

#入口
run



