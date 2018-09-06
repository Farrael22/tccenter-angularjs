/* EXEMPLO DE USO 
	var at = new Atalho(); // Instância do objeto 

	at.criarAtalho(['ctrl', 'enter'], function(){ //Atalho geral na página
		addOrcamento(true);
	});

	at.criarAtalho(['enter'], function(){ //Atalho se o foco estiver no campo #cpf
		$('#nome').focus();
	}, '#cpf');
	

	// Atalho em campo específico tem prioridade sobre atalhos de página
	// A passagem do seletor deve ser 
	//    #id
	//    .classe
	//    .classe1.classe2(...)
	//    #id.classe1.classe2.classe3(...)
*/

var $_at = null;
var Atalho = function () {
    var _ativos = [],
		_lista = [],
		_controle = [],
		_event,
        _time = null;

    var _as = [];
    _as['F1'] = 112;
    _as['F2'] = 113;
    _as['F3'] = 114;
    _as['F4'] = 115;
    _as['F5'] = 116;
    _as['F6'] = 117;
    _as['F7'] = 118;
    _as['F8'] = 119;
    _as['F9'] = 120;
    _as['F10'] = 121;
    _as['F11'] = 122;
    _as['F12'] = 123;
    _as['ctrl'] = 17;
    _as['shift'] = 16;
    _as['alt'] = 18;
    _as['tab'] = 9;
    _as['enter'] = 13;
    _as['del'] = 46;
    _as['delNumPad'] = 110;
    _as['up'] = 38;
    _as['down'] = 40;
    _as['left'] = 37;
    _as['right'] = 39;
    _as['home'] = 36;
    _as['end'] = 35;
    _as['pgup'] = 33;
    _as['pgdn'] = 34;
    _as['esc'] = 27;
    _as['backspace'] = 8;

    (function () {
        document.addEventListener("keyup", function (e) {
            _controle.pop();

            if (_controle.length == 0) {
                _lista = [];
            }
        });

        document.addEventListener("keydown", function (e) {
            
            if (_time != null) {
                clearTimeout(_time);
            }

            _time = setTimeout(function () {
                _controle = [];
                _lista = [];
            }, 2000);

            if (_lista[_lista.length - 1] != e.which && _controle[_controle.length - 1] != e.which) {
                _lista.push(e.which);
                _controle.push(e.which);

                var selector = '';
                //if (e.path[0].id != ''){
                //    var match = e.path[0].id.match(/_a-b-a_[a-zA-Z0-9-_]*/);

                //    if (match != null) selector = '#' + match[0];
                //}
                if (e.currentTarget.activeElement.className != '') {
                    var match = e.currentTarget.activeElement.className.match(/_a-b-a_[a-zA-Z0-9-_]*/);

                    if (match != null) selector = match[0];                    
                }

                if (_lerAtalho(_ativos, 0, selector, e.target)) {
                    e.preventDefault();
                    return false;
                }
            }
        });
    })();

    var _lerAtalho = function (array, pos, selector, target) {
        if (pos < _lista.length - 1) {
            if (typeof (array[_lista[pos]]) !== 'undefined') {
                return _lerAtalho(array[_lista[pos]], ++pos, selector, target);
            }
        } else {
            if (typeof (array[_lista[pos]]) !== 'undefined' && typeof (array[_lista[pos]][selector]) !== 'undefined' && typeof (array[_lista[pos]][selector].atalho) === 'function') {
                var prevent = array[_lista[pos]][selector].prevent;

                array[_lista[pos]][selector].atalho(target);
                _lista = [];

                return prevent;
            }
            else if (typeof (array[_lista[pos]]) !== 'undefined' && typeof (array[_lista[pos]].atalho) === 'function') {
                var prevent = array[_lista[pos]].prevent;

                array[_lista[pos]].atalho();
                _lista = [];

                return prevent;
            }

            return false;
        }
    }

    var _identificarTecla = function (item) {
        if (typeof (_as[item]) !== "undefined")
            return _as[item];

        else if (typeof (item) === 'string')
            return item.charCodeAt();

        else return item;
    }

    return {
        init: function () {
            if ($_at == null)
                $_at = new Atalho();
            return $_at;
        },
        criarAtalho: function (atalho, callback, selector, prevent) {

            var salvarAtalho = function (array, itens, pos, callback, selector, prevent) {

                var item = _identificarTecla(itens[pos]);

                if (pos < itens.length) {
                    if (array[item] == null) array[item] = [];
                    salvarAtalho(array[item], itens, ++pos, callback, selector, prevent);
                } else {
                    if (typeof (selector) !== 'undefined' && selector != null) {
                        var aba_selector = selector.replace(/\.|#/g, '_a-b-a_');

                        setTimeout(function () {
                            var el = document.querySelectorAll(selector);

                            for (var i = 0; i < el.length; i++) {
                                if (el[i].className.indexOf(aba_selector) === -1)
                                    el[i].className += ' ' + aba_selector;
                            }                           
                        }, 1);
                        
                        array[aba_selector] = [];
                        array[aba_selector]['atalho'] = callback;

                        if (prevent === false)
                            array[aba_selector]['prevent'] = false;
                        else
                            array[aba_selector]['prevent'] = true;

                    } else {
                        array['atalho'] = callback;

                        if (prevent === false)
                            array['prevent'] = false;
                        else
                            array['prevent'] = true;
                    }
                }
            };
            
            salvarAtalho(_ativos, atalho, 0, callback, selector, prevent);

            if (atalho && atalho.length > 1 && atalho[0] == 'ctrl' && atalho[1] == 'del') {
                atalho[1] = 'delNumPad';
                salvarAtalho(_ativos, atalho, 0, callback, selector, prevent);
            }
        }
    }
}