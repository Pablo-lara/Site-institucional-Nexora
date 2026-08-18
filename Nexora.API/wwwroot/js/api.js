// Substitua pela porta exata onde sua API ASP.NET Core está rodando
const API_BASE_URL = "/api";

async function apiFetch(endpoint, options = {}) {
    const token = localStorage.getItem("nexora_token");

    const headers = {
        "Content-Type": "application/json",
        ...options.headers
    };

    if (token) {
        headers["Authorization"] = `Bearer ${token}`;
    }

    const response = await fetch(`${API_BASE_URL}${endpoint}`, {
        ...options,
        headers
    });

    if (response.status === 401) {
        // Token expirado ou inválido
        localStorage.removeItem("nexora_token");
        if (window.location.pathname.includes("/admin/")) {
            window.location.href = "/admin/index.html";
        }
    }

    return response;
}