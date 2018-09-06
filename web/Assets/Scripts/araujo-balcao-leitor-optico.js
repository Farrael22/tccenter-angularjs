/* EXEMPLO DE USO 

	var lo = new LeitorOptico();
	lo.verificar('campo-busca', {
		success: function(value){ // caso tenha identificado a inserção por leitura óptica
			realizarBusca(value);

			$('#campo-busca').val('');
		},
		error: function(){ // caso a insersão não tenha sido por leitura óptica
			realizarBusca();
		}
	});
*/

var LeitorOptico = function(){
	var _controle = [],
		_value = '';


	return {
		verificar: function(id, callback){
			
			setTimeout(function () {
   
   				/*document.getElementById(id).addEventListener("keydown", function(e){

   				});*/

				document.getElementById(id).addEventListener("keydown", function(e){
					if(e.which == 13){
						timeM = _controle[_controle.length - 1] - _controle[_controle.length - 2];

						for(var i = _controle.length - 2; i > 0; i--){
							timeM = ((_controle[i] - _controle[i - 1]) + timeM) / 2;
						}

						var value_temp = _value;
						_controle = [];
						_value = '';

						if (timeM < 50) {
							if(typeof(callback.success) === 'function')
							    callback.success(value_temp)
						} else {
							if(typeof(callback.error) === 'function')
							    callback.error(value_temp)
						}
					} else {
					    _controle.push(new Date().getTime());
					    
					    if (_controle.length > 1 && (_controle[_controle.length - 1] - _controle[_controle.length - 2]) > 50) {
					        _controle = [];
					        _value = '';
					        _controle.push(new Date().getTime());
					    }
                        					
						_value += e.key;
					}
				});

			// your code here
			}, 1);
		}
	}
}