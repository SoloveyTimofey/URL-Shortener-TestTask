import React from 'react'
import ReactDOM from 'react-dom/client'
import ShortUrls from './components/ShortUrls.jsx'
import AddShortenedUrl from './components/AddShortenedUrl.jsx';

const root = ReactDOM.createRoot(document.getElementById('root'));

let addNewUrlHandler = null;

root.render(
    <React.StrictMode>
        <ShortUrls addNewUrlHandler={(handler) => (addNewUrlHandler = handler)} />
    </React.StrictMode>
);

const addShortenedUrlElement = document.getElementById('addShortenedUrl');
if (addShortenedUrlElement) {
    const addShortenedUrlRoot = ReactDOM.createRoot(addShortenedUrlElement);
    addShortenedUrlRoot.render(
        <React.StrictMode>
            <AddShortenedUrl onNewUrlAdded={(newUrl) => addNewUrlHandler && addNewUrlHandler(newUrl)} />
        </React.StrictMode>
    )
}