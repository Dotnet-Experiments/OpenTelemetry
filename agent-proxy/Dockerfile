
FROM nginx:alpine

WORKDIR /etc/nginx
COPY ./nginx.conf ./conf.d/default.conf
EXPOSE 4418
ENTRYPOINT [ "nginx" ]
CMD [ "-g", "daemon off;" ]
