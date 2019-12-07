print('启动GameInit.lua')

require "XLuaLogic/CtrlManager"

GameInit = {};
local this = GameInit;

function GameInit.InitViews()
	for i=1, #ViewNames do
		require('XLuaLogic/View/'..tostring(ViewNames[i]));
	end
end


function GameInit.Init()
	this.InitViews();
	CtrlManager.Init();
	GameInit.LoadView(CtrlNames.UIRootCtrl);
end

function GameInit.LoadView(type)
	local ctrl = CtrlManager.GetCtrl(type);
	if ctrl ~= nil then
		ctrl.Awake();
	end
end