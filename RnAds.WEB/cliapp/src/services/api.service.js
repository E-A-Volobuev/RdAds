class ApiService {

    async get(url, isBlob = false) {
        return await this.sendRequest(url, 'GET', undefined, isBlob);
    }

    async post(url, body, isBlob = false) {
        return await this.sendRequest(url, 'POST', body, isBlob);
    }

    async put(url, body) {
        return await this.sendRequest(url, 'PUT', body);
    }

    async delete(url) {
        return await this.sendRequest(url, 'DELETE');
    }

    async sendRequest(url, method, body, isBlob = false) {

        let options = {
            method: method,
            credentials: 'include'
        };

        if (body !== null) {
            options.body = body instanceof FormData ? body : JSON.stringify(body);

            if (!(body instanceof FormData))
                options.headers = {
                    "Content-Type": 'application/json'
                };
        }

        var fetchResponse = undefined;

        await fetch(url, options)
            .then(async (response) => {

                let responseError = undefined;
                let responseData = null;

                if (response.status === 200)
                    if (isBlob)
                        responseData = await response.blob();
                    else
                        responseData = await response.json();
                else {

                    switch (response.status) {
                        case 400:
                            {
                                let json = await response.json();
                                responseError = json.error ? json.error : 'Ошибка выполнения операции';
                                break;
                            }
                        case 401:
                            responseError = 'Ошибка авторизации';
                            break;
                        case 403:
                            responseError = 'Доступ запрещен';
                            break;
                        case 404:
                            responseError = 'Страница или запись не найдена, повторите операцию';
                            break;
                        default:
                            responseError = 'Ошибка сервера, повторите операцию';
                            break;
                    }
                }
                fetchResponse = { data: responseData, error: responseError };
                //return { data: responseData, error: responseError };
            });

        return fetchResponse;
    }
}

export default new ApiService()