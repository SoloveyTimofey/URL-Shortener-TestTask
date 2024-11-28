class UrlShortenerApiService {
    constructor(baseApiUrl) {
        this.baseApiUrl = baseApiUrl;
    }

    async getShortenedUrls() {
        try {
            const response = await fetch(`${this.baseApiUrl}/ShortenUrlApi/GetShortenedUrlsForUnathorizedUsers`, {
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            });

            if (!response.ok) {
                throw new Error(`Error fetching URLs: ${response.statusText}`);
            }

            return await response.json();
        } catch (error) {
            console.error('Error in getShortenedUrls:', error);
            throw error;
        }
    }

    async createShortenedUrl(originalUrl) {
        try {
            const response = await fetch(`${this.baseApiUrl}/shortenedUrls`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ originalUrl })
            });

            if (!response.ok) {
                throw new Error(`Error creating shortened URL: ${response.statusText}`);
            }

            return await response.json();
        } catch (error) {
            console.error('Error in createShortenedUrl:', error);
            throw error;
        }
    }

    async deleteShortenedUrl(id) {
        try {
            const response = await fetch(`${this.baseApiUrl}/shortenedUrlApi/DeleteShortenedUrl/${id}`, {
                method: 'DELETE',
                headers: { 'Content-Type': 'application/json' }
            });

            if (!response.ok) {
                throw new Error(`Error deleting URL: ${response.statusText}`);
            }

            return await response.json();
        } catch (error) {
            console.error('Error in deleteShortenedUrl:', error);
            throw error;
        }
    }
}

export default UrlShortenerApiService;