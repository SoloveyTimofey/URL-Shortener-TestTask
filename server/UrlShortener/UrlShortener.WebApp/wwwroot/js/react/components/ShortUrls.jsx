import React from "react";
import { useState, useEffect } from "react"
import { Table, Button } from 'react-bootstrap';
import UrlShortenerApiService from '../services/UrlShortenerApiService'
import './shortUrlsStyles.css';
import AppendShortenedUrlToBaseAppPath from '../utils/AppendShortenedUrlToBaseAppPath'

export default function ShortUrls({ addNewUrlHandler }) {
    const urlShortenerApiService = new UrlShortenerApiService('https://localhost:7248/api');
    
    const [shortenedUrls, setShortenedUrls] = useState([]);

    const handleAddNewUrl = (newUrl) => {
        setShortenedUrls((prevUrls) => [...prevUrls, newUrl]);
    };

    addNewUrlHandler(handleAddNewUrl);

    useEffect(() => {
        console.log("UseEffect....");

        const fetchShortenedUrls = async () => {
            try {
                const data = await urlShortenerApiService.getShortenedUrls();
                setShortenedUrls(data); 
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
                                <a href={AppendShortenedUrlToBaseAppPath(url.shortened)} target="_blank" rel="noopener noreferrer">
                                    {AppendShortenedUrlToBaseAppPath(url.shortened)}
                                </a>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>
        </div>
    )
}