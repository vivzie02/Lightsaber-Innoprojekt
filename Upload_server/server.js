require('dotenv').config();

let Client = require('ssh2-sftp-client');
const { Readable } = require('stream');


class SFTPClient {
    constructor() {
        this.client = new Client();
    }

    async connect(options) {
        console.log(`Connecting to ${options.host}:${options.port}, ${options.username} + ${options.password}`);
        try {
            await this.client.connect(options);
        } catch (err) {
            console.log('Failed to connect:', err);
        }
    }

    async disconnect() {
        try{
            await this.client.end();
            console.log("disconnected");
        }
        catch(err){
            console.log(err);
        }
        
    }

    async uploadFile(localFile, remoteFile) {
        console.log(`Uploading ${localFile} to ${remoteFile} ...`);
        try {
            const readable = new Readable();
            readable.push(localFile);
            readable.push(null);
            await this.client.put(readable, remoteFile);
            readable.destroy();
        } catch (err) {
            console.error('Uploading failed:', err);
        }
    }

}


(async () => {
    const parsedURL = new URL(process.env.SFTPTOGO_URL);
    const port = parsedURL.port || 22;
    const username = "innoapp";
    const password = "jGv9t^F5Nun*X6i4$97@";
    const host = "ftp-n2.cs.technikum-wien.at";

    const express = require('express');
    const bodyParser = require('body-parser');
    const fileUpload = require('express-fileupload');
    const app = express();

    // Use body-parser middleware to parse request bodies
    app.use(bodyParser.urlencoded({ extended: false }));
    app.use(express.json());
    app.use(fileUpload());

    app.post('/upload', async (req, res) => {
        console.log(req.body);
        console.log(req.files);
        
        //* Open the connection
        const client = new SFTPClient();
        await client.connect({ host, port, username, password });

        //* Upload local file to remote file
        await client.uploadFile(req.files.blobFile.data, `./Lightsaber/${req.files.blobFile.name}`);
    
        //* Close the connection
        await client.disconnect();
    });

    app.listen(3000, () => {
        console.log('Server started on port 3000');
    });
  
    
})();
