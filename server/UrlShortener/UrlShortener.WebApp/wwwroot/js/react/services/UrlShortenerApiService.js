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
}

export default UrlShortenerApiService;