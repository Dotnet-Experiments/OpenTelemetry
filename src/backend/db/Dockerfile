FROM mcr.microsoft.com/mssql/server:2022-latest

EXPOSE 1433

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=Test@12345
ENV MSSQL_PID=Developer
ENV MSSQL_TCP_PORT=1433 

# # Create non-root user and update permissions
# #
# RUN useradd -M -s /bin/bash -u 10001 -g 0 mssql
# RUN mkdir -p -m 770 /var/opt/mssql && chgrp -R 0 /var/opt/mssql

# # Grant sql the permissions to connect to ports <1024 as a non-root user
# #
# RUN setcap 'cap_net_bind_service+ep' /opt/mssql/bin/sqlservr

# # Allow dumps from the non-root process
# # 
# RUN setcap 'cap_sys_ptrace+ep' /opt/mssql/bin/paldumper
# RUN setcap 'cap_sys_ptrace+ep' /usr/bin/gdb

# # Add an ldconfig file because setcap causes the os to remove LD_LIBRARY_PATH
# # and other env variables that control dynamic linking
# #
# RUN mkdir -p /etc/ld.so.conf.d && touch /etc/ld.so.conf.d/mssql.conf
# RUN echo -e "# mssql libs\n/opt/mssql/lib" >> /etc/ld.so.conf.d/mssql.conf
# RUN ldconfig


# Create work directory
RUN mkdir -p /opt/mssql-data
WORKDIR /opt/mssql-data

# Copy all scripts into working directory
COPY . .

USER mssql
#CMD ["/opt/mssql/bin/sqlservr"]
CMD /bin/bash ./entrypoint.sh