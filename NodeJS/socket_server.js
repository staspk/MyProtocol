const net = require('net');
const readline = require('readline');

const consoleRead = readline.createInterface({
  input: process.stdin,
  output: process.stdout
});


const server = net.createServer((socket) => {
  console.log('.NET has connected!\n');

  socket.on('data', (data) => {
    console.log(`.NET: ${data.toString()}`);

    consoleRead.question(': ', (answer) => {
      socket.write(answer.toString());
      consoleRead.close();
    });
    
  });

  socket.on('end', () => {
    console.log('.NET disconnected. Shutting down server...');
    process.exit(0);
  });

  socket.on('error', (err) => {
    console.error('Socket error:', err);
  });
});

server.listen(8080, () => {
  console.log('Server listening on port 8080');
});