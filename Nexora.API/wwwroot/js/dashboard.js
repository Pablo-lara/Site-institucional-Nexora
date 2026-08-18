document.addEventListener("DOMContentLoaded", () => {
    // 1. Valida se o usuário está logado
    validarSessao();

    // 2. Exibe informações do perfil do admin logado
    const userJson = localStorage.getItem("nexora_user");
    if (userJson) {
        const user = JSON.parse(userJson);
        document.getElementById("user-nome").innerText = user.nome || "Administrador";
        document.getElementById("user-email").innerText = user.email || "";
    }

    // 3. Listener do botão de Logout
    document.getElementById("btn-logout").addEventListener("click", () => {
        logout();
    });

    // 4. Carrega os dados iniciais do dashboard
    carregarResumoDashboard();
});

document.addEventListener("DOMContentLoaded", () => {
    carregarServicosAdmin();
    carregarOrcamentos(); // <-- CHAMADA ADICIONADA AQUI

    document.getElementById("form-servico")?.addEventListener("submit", salvarServico);
});

async function carregarResumoDashboard() {
    try {
        // Carrega Orçamentos
        const resOrcamentos = await apiFetch("/orcamento");
        if (resOrcamentos.ok) {
            const orcamentos = await resOrcamentos.json();
            document.getElementById("total-orcamentos").innerText = orcamentos.length;
            renderizarTabelaOrcamentos(orcamentos);
        }

        // Carrega Serviços
        const resServicos = await apiFetch("/servico");
        if (resServicos.ok) {
            const servicos = await resServicos.json();
            document.getElementById("total-servicos").innerText = servicos.length;
        }

        // Carrega Projetos
        const resProjetos = await apiFetch("/projeto");
        if (resProjetos.ok) {
            const projetos = await resProjetos.json();
            document.getElementById("total-projetos").innerText = projetos.length;
        }
    } catch (error) {
        console.error("Erro ao carregar dados do dashboard:", error);
    }
}

async function carregarOrcamentos() {
    try {
        const res = await apiFetch("/orcamento/admin/todos", {method: "GET"});
        if (res.ok) {
            const rawData = await res.json();
            // Garante o tratamento caso venha dentro de um objeto (ex: { data: [...] })
            const orcamentos = Array.isArray(rawData) ? rawData : (rawData.data || []);
            renderizarTabelaOrcamentos(orcamentos);

            renderizarTabelaOrcamentos(orcamentos);

            // 2. Atualiza o card de contagem no topo do Dashboard
            const elContador = document.getElementById("qtd-orcamentos"); // Verifique se o ID no HTML é este
            if (elContador) {
                elContador.innerText = orcamentos.length;
            }
        }
    } catch (err) {
        console.error("Erro ao carregar orçamentos:", err);
    }
}

// Sua função de renderização ajustada para aceitar variações de nomes vindos do C#
function renderizarTabelaOrcamentos(orcamentos) {
    const tbody = document.getElementById("tabela-orcamentos");
    if (!tbody) return;

    if (!orcamentos || orcamentos.length === 0) {
        tbody.innerHTML = `
            <tr>
                <td colspan="5" style="text-align: center; color: #94a3b8;">Nenhum orçamento encontrado.</td>
            </tr>`;
        return;
    }

    tbody.innerHTML = orcamentos.map(o => {
        // Trata o nome do cliente
        const cliente = o.nomeCliente || o.nome || o.Nome || "Cliente";

        // Trata a data de forma segura sem quebrar o código JS
        let dataFormatada = "N/A";
        const dataRaw = o.dataSolicitacao || o.dataCriacao || o.data;
        if (dataRaw) {
            const parsedDate = new Date(dataRaw);
            if (!isNaN(parsedDate.getTime())) {
                dataFormatada = parsedDate.toLocaleDateString('pt-BR');
            }
        }

        return `
            <tr>
                <td><strong>${cliente}</strong></td>
                <td>${o.email || o.Email || '-'}</td>
                <td>${o.telefone || o.Telefone || 'N/A'}</td>
                <td>${dataFormatada}</td>
                <td><span class="badge badge-pendente">${o.status || 'Pendente'}</span></td>
            </tr>
        `;
    }).join("");
}

// Adicionar ao carregar a página
document.addEventListener("DOMContentLoaded", () => {
    // ... manter chamadas anteriores ...
    carregarServicosAdmin();

    document.getElementById("form-servico").addEventListener("submit", salvarServico);
});

async function carregarServicosAdmin() {
    const res = await apiFetch("/servico");
    if (res.ok) {
        const servicos = await res.json();
        const tbody = document.getElementById("tabela-servicos-admin");

        if (servicos.length === 0) {
            tbody.innerHTML = `<tr><td colspan="3" style="text-align:center;">Nenhum serviço cadastrado.</td></tr>`;
            return;
        }

        tbody.innerHTML = servicos.map(s => `
            <tr>
                <td><strong>${s.nome}</strong></td>
                <td>${s.descricao}</td>
                <td>
                    <button onclick="excluirServico(${s.id})" style="color:#ef4444; border:none; background:none; cursor:pointer; font-weight:bold;">Excluir</button>
                </td>
            </tr>
        `).join("");
    }
}

function abrirModalServico() {
    document.getElementById("servico-id").value = "";
    document.getElementById("servico-nome").value = "";
    document.getElementById("servico-descricao").value = "";
    document.getElementById("modal-servico").style.display = "flex";
}

function fecharModalServico() {
    document.getElementById("modal-servico").style.display = "none";
}

async function salvarServico(e) {
    e.preventDefault();
    const id = document.getElementById("servico-id").value;
    const nome = document.getElementById("servico-nome").value;
    const descricao = document.getElementById("servico-descricao").value;

    const payload = { nome, descricao };
    const method = id ? "PUT" : "POST";
    const endpoint = id ? `/servico/${id}` : "/servico";

    const res = await apiFetch(endpoint, {
        method,
        body: JSON.stringify(payload)
    });

    if (res.ok) {
        fecharModalServico();
        carregarServicosAdmin();
        carregarResumoDashboard();
    } else {
        alert("Erro ao salvar o serviço.");
    }
}

async function excluirServico(id) {
    if (!confirm("Tem certeza que deseja excluir este serviço?")) return;

    const res = await apiFetch(`/servico/${id}`, { method: "DELETE" });
    if (res.ok) {
        carregarServicosAdmin();
        carregarResumoDashboard();
    } else {
        alert("Erro ao excluir o serviço.");
    }
}

document.addEventListener("DOMContentLoaded", () => {
    // ... manter chamadas existentes ...
    carregarProjetosAdmin();
    carregarArtigosAdmin();

    document.getElementById("form-projeto").addEventListener("submit", salvarProjeto);
    document.getElementById("form-artigo").addEventListener("submit", salvarArtigo);
});

/* ================= CRUD PROJETOS ================= */

// Registra o evento de envio do formulário assim que o script carrega
document.getElementById("form-projeto")?.addEventListener("submit", salvarProjeto);

async function carregarProjetosAdmin() {
    const res = await apiFetch("/projeto");
    if (res.ok) {
        const projetos = await res.json();
        const tbody = document.getElementById("tabela-projetos-admin");

        if (!projetos || projetos.length === 0) {
            tbody.innerHTML = `<tr><td colspan="4" style="text-align:center;">Nenhum projeto cadastrado.</td></tr>`;
            return;
        }

        tbody.innerHTML = projetos.map(p => `
            <tr>
                <td><strong>${p.nome || p.titulo || 'Sem título'}</strong></td>
                <td>${p.cliente || 'N/A'}</td>
                <td>${p.destaque ? '<span class="badge badge-aprovado">Sim</span>' : 'Não'}</td>
                <td>
                    <button onclick="excluirProjeto(${p.id})" style="color:#ef4444; border:none; background:none; cursor:pointer; font-weight:bold;">Excluir</button>
                </td>
            </tr>
        `).join("");
    }
}

function abrirModalProjeto() {
    document.getElementById("form-projeto").reset();
    document.getElementById("modal-projeto").style.display = "flex";
}

function fecharModalProjeto() {
    document.getElementById("modal-projeto").style.display = "none";
}

async function salvarProjeto(e) {
    e.preventDefault();

    const textoDescricao = document.getElementById("projeto-descricao").value;

    // Ajustado de 'titulo' para 'nome' conforme exigido pela API C#
    const payload = {
        nome: document.getElementById("projeto-titulo").value,
        descricaoResumida: textoDescricao,
        descricao: textoDescricao,
        cliente: document.getElementById("projeto-cliente").value || null,
        destaque: document.getElementById("projeto-destaque").checked
    };

    try {
        const res = await apiFetch("/projeto", {
            method: "POST",
            body: JSON.stringify(payload)
        });

        if (res.ok) {
            fecharModalProjeto();
            await carregarProjetosAdmin();
            if (typeof carregarResumoDashboard === "function") {
                await carregarResumoDashboard();
            }
        } else {
            const erroData = await res.json();
            if (erroData.errors) {
                const mensagens = Object.entries(erroData.errors)
                    .map(([campo, msgs]) => `${campo}: ${msgs.join(", ")}`)
                    .join("\n");
                alert(`Erro de Validação:\n\n${mensagens}`);
            } else {
                alert("Erro ao salvar projeto.");
            }
        }
    } catch (err) {
        console.error("Erro na requisição:", err);
    }
}

async function excluirProjeto(id) {
    if (!confirm("Deseja excluir este projeto?")) return;
    const res = await apiFetch(`/projeto/${id}`, { method: "DELETE" });
    if (res.ok) {
        await carregarProjetosAdmin();
        if (typeof carregarResumoDashboard === "function") {
            await carregarResumoDashboard();
        }
    }
}

/* ================= CRUD ARTIGOS ================= */
async function carregarArtigosAdmin() {
    const res = await apiFetch("/artigo/admin/todos"); // Rota que traz rascunhos e publicados
    if (!res.ok) return;

    const artigos = await res.json();
    const tbody = document.getElementById("tabela-artigos-admin");

    if (artigos.length === 0) {
        tbody.innerHTML = `<tr><td colspan="4" style="text-align:center;">Nenhum artigo publicado.</td></tr>`;
        return;
    }

    tbody.innerHTML = artigos.map(a => `
        <tr>
            <td><strong>${a.titulo}</strong></td>
            <td>${a.publicado ? '<span class="badge badge-aprovado">Publicado</span>' : '<span class="badge badge-pendente">Rascunho</span>'}</td>
            <td>${new Date(a.dataPublicacao).toLocaleDateString('pt-BR')}</td>
            <td>
                <button onclick="excluirArtigo(${a.id})" style="color:#ef4444; border:none; background:none; cursor:pointer; font-weight:bold;">Excluir</button>
            </td>
        </tr>
    `).join("");
}

function abrirModalArtigo() { document.getElementById("modal-artigo").style.display = "flex"; }
function fecharModalArtigo() { document.getElementById("modal-artigo").style.display = "none"; }

async function salvarArtigo(e) {
    e.preventDefault();
    const payload = {
        titulo: document.getElementById("artigo-titulo").value,
        resumo: document.getElementById("artigo-resumo").value,
        conteudo: document.getElementById("artigo-conteudo").value,
        publicado: document.getElementById("artigo-publicado").checked
    };

    const res = await apiFetch("/artigo", { method: "POST", body: JSON.stringify(payload) });
    if (res.ok) {
        fecharModalArtigo();
        carregarArtigosAdmin();
    }
}

async function excluirArtigo(id) {
    if (!confirm("Deseja excluir este artigo?")) return;
    const res = await apiFetch(`/artigo/${id}`, { method: "DELETE" });
    if (res.ok) carregarArtigosAdmin();
}