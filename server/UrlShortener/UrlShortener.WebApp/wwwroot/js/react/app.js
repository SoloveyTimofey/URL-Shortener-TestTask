import React from 'react'
import ReactDOM from 'react-dom/client'
import ShortUrls from './components/ShortUrls.jsx'

console.log("React is rendering...");
const root = ReactDOM.createRoot(document.getElementById('root'));

root.render(
    <React.StrictMode>
        <ShortUrls />
    </React.StrictMode>
    //<div>
    //    Helllo
    //</div>
);