
// Preview da foto do paciente
function exibirFoto(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            var preview = document.getElementById('preview');
            preview.src = e.target.result;
            preview.style.display = 'block';
        };

        reader.readAsDataURL(input.files[0]);
    }
}

function fecharModalAtualizarEndereco() {
    var closeButton = document.getElementById('botaoFecharEndereco');

    closeButton.addEventListener('click', function () {
        var modal = document.querySelector('.modal');
        var backdrop = document.querySelector('.modal-backdrop');

        // Fecha a modal removendo as classes "show" e "modal-open"
        modal.classList.remove('show');
        backdrop.classList.remove('show');

        // Remove o conteúdo da modal
        modal.innerHTML = '';
    });
}


function buscaCep() {
    const limparFormularioCep = () => {
        document.querySelector("#Endereco_Logradouro").value = "";
        document.querySelector("#Endereco_Bairro").value = "";
        document.querySelector("#Endereco_Cidade").value = "";
        document.querySelector("#Endereco_Estado").value = "";
    };

    const preencherCamposEndereco = (dados) => {
        document.querySelector("#Endereco_Logradouro").value = dados.logradouro;
        document.querySelector("#Endereco_Bairro").value = dados.bairro;
        document.querySelector("#Endereco_Cidade").value = dados.localidade;
        document.querySelector("#Endereco_Estado").value = dados.uf;
    };

    const mostrarMensagemErro = () => {
        limparFormularioCep();
        alert("CEP não encontrado.");
    };

    const validarCep = (cep) => {
        return /^[0-9]{8}$/.test(cep);
    };

    const consultarCep = async (cep) => {
        try {
            const response = await fetch(`https://viacep.com.br/ws/${cep}/json/`);
            const dados = await response.json();

            if (!dados.erro) {
                preencherCamposEndereco(dados);
            } else {
                mostrarMensagemErro();
            }
        } catch (error) {
            mostrarMensagemErro();
        }
    };

    const campoCep = document.querySelector("#Endereco_Cep");
    campoCep.addEventListener("blur", () => {
        const cep = campoCep.value.replace(/\D/g, "");

        if (cep !== "" && validarCep(cep)) {
            limparFormularioCep();
            consultarCep(cep);
        } else {
            limparFormularioCep();
            alert("Formato de CEP inválido.");
        }
    });
}


//function BuscaCep() {
//    $(document).ready(function () {

//        function limpa_formulário_cep() {
//            // Limpa valores do formulário de cep.
//            $("#Endereco_Logradouro").val("");
//            $("#Endereco_Bairro").val("");
//            $("#Endereco_Cidade").val("");
//            $("#Endereco_Estado").val("");
//        }

//        //Quando o campo cep perde o foco.
//        $("#Endereco_Cep").blur(function () {

//            //Nova variável "cep" somente com dígitos.
//            var cep = $(this).val().replace(/\D/g, '');

//            //Verifica se campo cep possui valor informado.
//            if (cep != "") {

//                //Expressão regular para validar o CEP.
//                var validacep = /^[0-9]{8}$/;

//                //Valida o formato do CEP.
//                if (validacep.test(cep)) {

//                    //Preenche os campos com "..." enquanto consulta webservice.
//                    $("#Endereco_Logradouro").val("...");
//                    $("#Endereco_Bairro").val("...");
//                    $("#Endereco_Cidade").val("...");
//                    $("#Endereco_Estado").val("...");

//                    //Consulta o webservice viacep.com.br/
//                    $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?",
//                        function (dados) {

//                            if (!("erro" in dados)) {
//                                //Atualiza os campos com os valores da consulta.
//                                $("#Endereco_Logradouro").val(dados.logradouro);
//                                $("#Endereco_Bairro").val(dados.bairro);
//                                $("#Endereco_Cidade").val(dados.localidade);
//                                $("#Endereco_Estado").val(dados.uf);
//                            } //end if.
//                            else {
//                                //CEP pesquisado não foi encontrado.
//                                limpa_formulário_cep();
//                                alert("CEP não encontrado.");
//                            }
//                        });
//                } //end if.
//                else {
//                    //cep é inválido.
//                    limpa_formulário_cep();
//                    alert("Formato de CEP inválido.");
//                }
//            } //end if.
//            else {
//                //cep sem valor, limpa formulário.
//                limpa_formulário_cep();
//            }
//        });
//    });
//}

function setModal() {
    $(document).ready(function () {
        $("a[data-modal]").on("click", function (e) {
            e.preventDefault();
            const $myModalContent = $('#myModalContent');
            const url = this.href;

            $myModalContent.load(url, function () {
                $('#myModal').modal('show');
                bindForm($myModalContent);
            });
        });
    });
}

function bindForm($dialog) {
    $dialog.find('form').submit(function (e) {
        e.preventDefault();
        const $form = $(this);
        const url = $form.attr('action');
        const method = $form.attr('method');
        const formData = $form.serialize();

        $.ajax({
            url: url,
            type: method,
            data: formData,
            success: function (result) {
                if (result.success) {
                    $('#myModal').modal('hide');
                    $('#EnderecoTarget').load(result.url);
                } else {
                    $('#myModalContent').html(result);
                    bindForm($dialog);
                }
            }
        });
    });
}

setModal();
