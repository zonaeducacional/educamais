import pty
import os
import sys

password = sys.argv[1]
command = "mkdir -p ~/.ssh && echo 'ssh-rsa AAAAB3NzaC1yc2EAAAADAQABAAACAQC35G5/C78g1iCqglSjjA7yBWbCH55DZfyU8gvRLhqYzz/4ulgYagUqsyRc4MgEAIK4Raap17Kwt3tkSewjtjvIxZ1tvYZ4PBdSjkHBTpU5qKK/jDkOBX5OaKQRmHOUxM2myyDteWcty6AsQGu19r57aXzkf41XtwM2HUMS/ZaJajQdOVjTK3AtEbBrLBfdyZrLMMYgIg8L8wqXfAj6NZ68z0wQuw7n32DqSwhAneKqrkZlumMmGEjibAZaHptnA5SON6Y66wmHjXsg0f4XwWr24gxuWQyIqCfVct/WP2cI6jdNwABIgeNBdZtPvAwNKHCiEa0JEJgesnEbVu5Y4qJN5MlP2o8dUkt/xR692PAIek5EppEkGJUI26UCIfXwSGSPRy+3epkpjwGGAPAGR15P7Hy5CMyB8z0bqRQ8gr0z/DB/FtV3gslyO0vt57vqdcTLag6GCGhA4AFXpu5fkpj6lz+klKBudm6ZrOjTpWucdwk2bm0svRgfe9XykQgU5F8S1qnsN7J+OYhO5CpqBQxb8c5kWuvDJehlpSN7KHzGHoYkZkzZ5FR1iMdLYlSByKzS1sMCS5k8HzHfiJ4xMgHi9mOlBfD8oI534LV8CmJGQipbwTt3oXxPpvadXv1oQ0TsswyMeQKN6tyPBgMQ9qWwSR2ojTrHfvpmruofc0hAdw== sergio@sergio-Inspiron-15-3520' >> ~/.ssh/authorized_keys && chmod 700 ~/.ssh && chmod 600 ~/.ssh/authorized_keys"

pid, fd = pty.fork()
if pid == 0:
    os.execv('/usr/bin/ssh', ['ssh', '-o', 'StrictHostKeyChecking=no', 'server@192.168.0.200', command])
else:
    output = b""
    while True:
        try:
            data = os.read(fd, 1024)
            if not data:
                break
            output += data
            if b'assword:' in data:
                os.write(fd, password.encode() + b'\n')
        except OSError:
            break
    print(output.decode(errors='replace'))
