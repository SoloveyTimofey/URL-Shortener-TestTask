//import React from 'react';
//import { Button, Form } from 'react-bootstrap';

//export default function AddShortenedUrl() {
//    return (
//        <div className="mt-2">
//            <Form>
//                <Form.Group className="d-flex align-items-center">
//                    <Form.Control
//                        type="text"
//                        placeholder="Enter URL to be shortened..."
//                        className="me-2"
//                    />
//                    <Button variant="primary" type="submit" className="h-100">
//                        Shorten URL
//                    </Button>
//                </Form.Group>
//            </Form>
//        </div>
//    );
//}

import React, { useState } from 'react';
import { Button, Form } from 'react-bootstrap';
import AppendShortenedUrlToBaseAppPath from '../utils/AppendShortenedUrlToBaseAppPath'

export default function AddShortenedUrl({ onNewUrlAdded }) {
    const [url, setUrl] = useState('');
    const [isLoading, setIsLoading] = useState(false);
    const [responseMessage, setResponseMessage] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();
        setIsLoading(true);
        setResponseMessage('');

        //TODO: Replace this piece to UrlShortenerApiService
        try {
            const response = await fetch(`https://localhost:7248/api/ShortenUrlApi/CreateShortenedUrl?originalUrl=${encodeURIComponent(url)}`, {
                method: 'POST',
            });

            if (response.ok) {
                const data = await response.json();
                setResponseMessage(`Shortened URL: ${AppendShortenedUrlToBaseAppPath(data.shortened)}`);
                onNewUrlAdded(data);
            } else {
                const errorData = await response.json();
                setResponseMessage(`Error: ${errorData.message || 'Failed to shorten URL'}`);
            }
        } catch (error) {
            setResponseMessage(`Error: ${error.message}`);
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <div className="mt-2">
            <Form onSubmit={handleSubmit}>
                <Form.Group className="d-flex align-items-center">
                    <Form.Control
                        type="text"
                        placeholder="Enter URL to be shortened..."
                        value={url}
                        onChange={(e) => setUrl(e.target.value)}
                        className="me-2"
                    />
                    <Button variant="primary" type="submit" className="h-100" disabled={isLoading}>
                        {isLoading ? 'Processing...' : 'Shorten URL'}
                    </Button>
                </Form.Group>
            </Form>
            {responseMessage && <p className="mt-3">{responseMessage}</p>}
        </div>
    );
}