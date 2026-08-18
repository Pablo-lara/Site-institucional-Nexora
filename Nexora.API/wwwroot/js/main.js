document.addEventListener("DOMContentLoaded", () => {
    carregarServicosPublico();
    carregarProjetosPublico();
    carregarArtigosPublico();

    document.getElementById("form-orcamento")?.addEventListener("submit", enviarOrcamento);
});

/* Helper para extrair array independente do formato retornado pela API C# */
function extrairArray(json) {
    if (Array.isArray(json)) return json;
    if (json && Array.isArray(json.data)) return json.data;
    if (json && Array.isArray(json.result)) return json.result;
    if (json && Array.isArray(json.items)) return json.items;
    return [];
}

/* ================= CARREGAR SERVIÇOS ================= */
async function carregarServicosPublico() {
    const grid = document.getElementById("grid-servicos");
    try {
        const res = await apiFetch("/servico");
        if (res.ok) {
            const rawData = await res.json();
            const servicos = extrairArray(rawData);

            if (servicos.length === 0) {
                grid.innerHTML = `<p style="text-align:center; grid-column: 1/-1; color:#94a3b8;">Nenhum serviço disponível no momento.</p>`;
                return;
            }

            grid.innerHTML = servicos.map(s => `
                <div class="card">
                    <div>
                        <h3>${s.nome || s.Nome || 'Serviço'}</h3>
                        <p>${s.descricao || s.Descricao || ''}</p>
                    </div>
                    <span class="card-tag">Serviço Especializado</span>
                </div>
            `).join("");
        }
    } catch (err) {
        console.error("Erro ao carregar serviços:", err);
    }
}

/* ================= CARREGAR PROJETOS ================= */
async function carregarProjetosPublico() {
    const grid = document.getElementById("grid-projetos");
    try {
        const res = await apiFetch("/projeto");
        if (res.ok) {
            const rawData = await res.json();
            const projetos = extrairArray(rawData);

            if (projetos.length === 0) {
                grid.innerHTML = `<p style="text-align:center; grid-column: 1/-1; color:#94a3b8;">Nenhum projeto cadastrado.</p>`;
                return;
            }

            grid.innerHTML = projetos.map(p => `
                <div class="card">
                    <div>
                        <span class="card-tag">${p.cliente || p.Cliente || 'Caso de Sucesso'}</span>
                        <h3 style="margin-top:0.5rem;">${p.nome || p.Nome || p.titulo || p.Titulo}</h3>
                        <p>${p.descricaoResumida || p.DescricaoResumida || p.descricao || p.Descricao || ''}</p>
                    </div>
                </div>
            `).join("");
        }
    } catch (err) {
        console.error("Erro ao carregar projetos:", err);
    }
}

/* ================= CARREGAR ARTIGOS ================= */

let artigosCache = [];

async function carregarArtigosPublico() {
    const grid = document.getElementById("grid-artigos");
    if (!grid) return;

    try {
        const res = await apiFetch("/artigo");
        if (res.ok) {
            const rawData = await res.json();
            artigosCache = Array.isArray(rawData) ? rawData : (rawData.data || rawData.result || []);

            if (artigosCache.length === 0) {
                grid.innerHTML = `<p style="text-align:center; grid-column: 1/-1; color:#94a3b8;">Nenhum artigo publicado ainda.</p>`;
                return;
            }

            grid.innerHTML = artigosCache.map((a, index) => {
                const titulo = a.titulo || a.Titulo || 'Sem título';
                const resumo = a.resumo || a.Resumo || a.conteudo || a.Conteudo || '';

                return `
                    <div class="card">
                        <div>
                            <h3>${titulo}</h3>
                            <p>${resumo.length > 120 ? resumo.substring(0, 120) + '...' : resumo}</p>
                        </div>
                        <div style="display:flex; justify-content:space-between; align-items:center; margin-top:1rem;">
                            <span class="card-tag">Artigo</span>
                            <button type="button" onclick="lerArtigoCompleto(${index})" style="background:none; border:none; color:#0284c7; font-weight:600; cursor:pointer;">
                                Ler completo &rarr;
                            </button>
                        </div>
                    </div>
                `;
            }).join("");
        }
    } catch (err) {
        console.error("Erro ao carregar artigos:", err);
    }
}

// Expõe a função no escopo global para garantir o clique
window.lerArtigoCompleto = function (index) {
    const artigo = artigosCache[index];
    if (!artigo) return;

    const elTitulo = document.getElementById("artigo-modal-titulo");
    const elData = document.getElementById("artigo-modal-data");
    const elConteudo = document.getElementById("artigo-modal-conteudo");
    const modal = document.getElementById("modal-artigo-leitura");

    if (elTitulo) elTitulo.innerText = artigo.titulo || artigo.Titulo || "";

    const dataRaw = artigo.dataCriacao || artigo.DataCriacao || artigo.data;
    if (elData) {
        elData.innerText = dataRaw ? `Publicado em: ${new Date(dataRaw).toLocaleDateString("pt-BR")}` : "";
    }

    if (elConteudo) {
        elConteudo.innerText = artigo.conteudo || artigo.Conteudo || artigo.resumo || artigo.Resumo || "";
    }

    if (modal) {
        modal.style.display = "flex";
    }
};

window.fecharModalArtigoLeitura = function () {
    const modal = document.getElementById("modal-artigo-leitura");
    if (modal) {
        modal.style.display = "none";
    }
};

/* ================= ENVIAR ORÇAMENTO ================= */
async function enviarOrcamento(e) {
    e.preventDefault();

    const textoMensagem = document.getElementById("cliente-mensagem").value.trim();
    const nomeInput = document.getElementById("cliente-nome").value.trim();


    // Enviamos 'descricao' e 'mensagem' para atender qualquer nomenclatura na DTO C#
    const payload = {
        nome: document.getElementById("cliente-nome").value.trim(),
        nomeCliente: nomeInput,
        email: document.getElementById("cliente-email").value.trim(),
        telefone: document.getElementById("cliente-telefone").value.trim() || null,
        descricao: textoMensagem,
        mensagem: textoMensagem,
        dataSolicitacao: new Date().toISOString(),
        status: "Pendente"
    };

    try {
        const res = await apiFetch("/orcamento", {
            method: "POST",
            body: JSON.stringify(payload)
        });

        if (res.ok) {
            alert("Solicitação enviada com sucesso! Entraremos em contato em breve.");
            document.getElementById("form-orcamento").reset();
        } else {
            const erroData = await res.json();
            if (erroData.errors) {
                const msgs = Object.values(erroData.errors).flat().join("\n");
                alert(`Erro de Validação:\n${msgs}`);
            } else {
                alert("Erro ao enviar solicitação: " + (erroData.mensagem || "Verifique os dados."));
            }
        }
    } catch (err) {
        console.error("Erro no envio do orçamento:", err);
        alert("Falha de comunicação com o servidor.");
    }
}