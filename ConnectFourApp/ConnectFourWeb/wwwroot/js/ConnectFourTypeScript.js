const a = "a";
const b = "b";
let currentTurn = a;
let gameover = false;
let startgame = false;
let msg;
let allspots = [];
let topspots;
let btnStart;
let colorselectora;
let colorselectorb;
const winningSets = [];
const columns = [];
$(document).ready(function () {
    msg = document.querySelector("#msg");
    allspots = [...$(".spot")];
    topspots = [...$(".top")];
    colorselectora = document.querySelector("#colorselectora");
    colorselectora.addEventListener("input", setColor);
    colorselectorb = document.querySelector("#colorselectorb");
    colorselectorb.addEventListener("input", setColor);
    SetupButtons();
    btnStart = document.querySelector("#btnStart");
    $(btnStart).click(StartGame);
    topspots.forEach(s => s.addEventListener("click", TakeSpot));
    msg.textContent = "Click Start to begin the game.";
});
function setColor() {
    for (let i = 0; i < document.styleSheets.length; i++) {
        const sheet = document.styleSheets[i];
        for (let j = 0; j < sheet.cssRules.length; j++) {
            const rule = sheet.cssRules[j];
            if (rule instanceof CSSStyleRule) {
                if (rule.selectorText == ".colora") {
                    rule.style.backgroundColor = colorselectora.value;
                }
                else if (rule.selectorText == ".colorb") {
                    rule.style.backgroundColor = colorselectorb.value;
                }
            }
        }
    }
}
function SetupButtons() {
    for (let i = 1; i < 19; i++) {
        winningSets.push([...$(`.h${i}`)]);
        winningSets.push([...$(`.v${i}`)]);
    }
    for (let i = 1; i < 7; i++) {
        columns.push([...$(`.c${i}`)].reverse());
    }
}
function StartGame() {
    gameover = false;
    startgame = true;
    $(allspots).removeClass("colora").removeClass("colorb").removeClass("tie").removeClass("winner").removeClass("disabled").addClass("startspot");
    currentTurn = a;
    showCurrentTurn();
}
function TakeSpot(event) {
    let target = event.target;
    let col = columns.find(c => c.includes(target));
    let spot = col.find(s => s.classList.contains("startspot"));
    if (startgame == true) {
        $(spot).removeClass("startspot");
        currentTurn == a ? $(spot).addClass("colora") : $(spot).addClass("colorb");
        CheckTieWinner();
        if (gameover == false) {
            currentTurn = currentTurn == a ? b : a;
            showCurrentTurn();
        }
    }
}
function CheckTieWinner() {
    winningSets.forEach(w => {
        if (w.every(s => s.classList.contains("colora")) || w.every(s => s.classList.contains("colorb"))) {
            gameover = true;
            $(w).addClass("winner");
            msg.textContent = "Winner is Player " + currentTurn + "!";
            $(allspots).addClass("disabled");
        }
    });
    if (gameover == false) {
        if (allspots.every(s => s.classList.contains("startspot") != true)) {
            gameover = true;
            msg.textContent = "Tie!";
            $(allspots).removeClass("colora").removeClass("colorb").addClass("tie");
        }
    }
}
function showCurrentTurn() {
    msg.textContent = "Player " + currentTurn + "! It's your turn...";
}
//# sourceMappingURL=ConnectFourTypeScript.js.map