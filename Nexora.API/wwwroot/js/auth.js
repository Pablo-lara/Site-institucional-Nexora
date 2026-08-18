async function fazerLogin(email, senha) {
    const response = await apiFetch("/auth/login", {
        method: "POST",
        body: JSON.stringify({ email, senha })
    });

    if (!response.ok) {
        const erro = await response.json().catch(() => ({}));
        throw new Error(erro.mensagem || "Falha na autenticação. Verifique suas credenciais.");
    }

    const data = await response.json();
    localStorage.setItem("nexora_token", data.token);
    localStorage.setItem("nexora_user", JSON.stringify(data));

    window.location.href = "dashboard.html";
}

function logout() {
    localStorage.removeItem("nexora_token");
    localStorage.removeItem("nexora_user");
    window.location.href = "index.html";
}

function validarSessao() {
    const token = localStorage.getItem("nexora_token");
    if (!token) {
        window.location.href = "index.html";
    }
}