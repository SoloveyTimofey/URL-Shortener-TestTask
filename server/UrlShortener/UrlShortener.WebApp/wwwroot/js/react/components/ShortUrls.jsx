import React from "react";
import { useState, useEffect } from "react"
import { Table, Button } from 'react-bootstrap';
import UrlShortenerApiService from '../services/UrlShortenerApiService'
import './shortUrlsStyles.css'; 

export default function ShortUrls() {
    const urlShortenerApiService = new UrlShortenerApiService('https://localhost:7248/api');
    
    const [shortenedUrls, setShortenedUrls] = useState([]);

    useEffect(() => {
        console.log("UseEffect....");

        const fetchShortenedUrls = async () => {
            try {
                const data = await urlShortenerApiService.getShortenedUrls();
                setShortenedUrls(data); 
                console.log(data); 
            } catch (error) {
                console.error("Error fetching shortened URLs:", error);
            }
        };

        fetchShortenedUrls();
    }, []);

    return (
        <div className="container-fluid mt-5">
            <h2>Shortened URLs</h2>
            <Table striped bordered hover className="table-responsive">
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Original URL</th>
                        <th>Shortened URL</th>
                        <th style={{ width: '150px' }}>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {shortenedUrls.map((url, index) => (
                        <tr key={url.id}>
                            <td>{index + 1}</td>
                            <td className="text-wrap">
                                <a href={url.originalUrl} target="_blank" rel="noopener noreferrer">
                                    {url.originalUrl}
                                </a>
                            </td>
                            <td className="text-wrap">
                                <a href={url.shortenedUrl} target="_blank" rel="noopener noreferrer">
                                    {url.shortened}
                                </a>
                            </td>
                            <td style={{ width: '150px' }}>
                                <Button variant="danger" className="btn-sm text-sm-start" onClick={() => handleDelete(url.id)}>
                                    Delete
                                </Button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>
        </div>


    )

    function handleDelete(id) {
        if (window.confirm("Are you sure you want to delete this URL?")) {
            // Реализуйте удаление записи с помощью API
            console.log("Deleted URL with ID:", id);
        }
    }
}