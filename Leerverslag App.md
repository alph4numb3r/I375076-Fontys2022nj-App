<div style= "page-break-after: always;text-align:center">
<h1>Leerverslag Applicatie</h1>
<h2>Benjamin van der Grift (3170756)</h2>
<h2>I-OE2-DB, Docent: Michielsen, Bas B.S.H.T.</h2>
<img title="Placeholder" src="S:/Pictures/IMG_0292.JPG"/>
</div>


# Inhoudsopgave <!-- omit in toc -->

- [1. Introductie](#1-introductie)
- [2. Verslag](#2-verslag)
- [Bijlages](#bijlages)
- [A. Versiebeheer](#a-versiebeheer)

# 1. Introductie
Ik ben Benjamin van der Grift, 24 jaar oud, en dit is helaas al mijn derde studie.  
Ik heb al van jongs af aan veel ervaring opgedaan met computers, en de smaak autisme die ik heb zorgt er voor dat ik heel snel nieuwe (technische) dingen op kan pakken, dus mijn kennis is altijd al heel groot geweest op het gebied van ICT.

Dit is mijn derde poging tot het halen van S2 infra, de eerste keer (vj2021) kon ik me gewoon niet motiveren voor het 100% online onderwijs, de tweede keer liep ik na een paar weken vast vanwege persoonlijke situaties, hopelijk lukt het deze keer wel!

# 2. Verslag

Om te beginnen heb ik Git & Go bijgewerkt en mijn [repo](https://git.fhict.nl/I375076/go) schoon gemaakt.

Ik heb besloten om 1 hoofdprogramma te maken met menus voor de rest m.b.v. [cview](https://code.rocketnine.space/tslocum/cview).  
Ik vind het zelf fijn om dingen via een terminal toegankelijk te maken als dat mogelijk is, want dat maakt het mogelijk om het over SSH te gebruiken wat voor remote management best handig is (i.p.v. het over een X server pijp te gooien o.i.d.). Na even googleen leek me deze library het meest geschikt daarvoor.

## Priemcalculator

1e sub-programma: een priemcalculator! (Eigenlijk een programma dat de factoren van een getal bepaalt, maar dat komt er ook achter of t een priem is). Meestal is dit het tweede ding dat ik in een nieuwe taal maak na 'Hello World', want het is een goede oefening van hoe loops, variabelen, arrays, functies enz. werken. Dit lukte redelijk makkelijk nadat ik door had hoe types en het omzetten daarvan werken in Go.

Het toevoegen hiervan aan mijn hoofdprogramma ging ook redelijk vlot! Het was even zoeken hoe ik een menu maakte want de documentatie van cview is niet geweldig, maar de rest ging gewoon goed.

## Poort sniffer

2e sub-programma (1e 'opdracht'): een poort sniffer!
Leek me best simpel te implementeren, is ook zo geweest! Eerst uitgevogeld hoe de 'net' library werkte in Go, daarna...

## Gesprek met Bas
Feedback-gesprek gehad met Bas, over dat de logica achter mijn programma niet duidelijk was (namelijk waarom ik zoveel framework om zo'n simpel programma had). Hij heeft mij erop gewezen dat ik beter nu met simpele code de rubrieken (complexiteit, configureerbaarheid, robuustheid, deployment) aan kan wijzen en dan daarna (in de challenge) helemaal lost kan gaan.

Tijd om de poort sniffer dus om te bouwen naar een simpel ding waarmee ik de rubrieken aan kan wijzen! Plan: cli programma voor port scanning en DNS, met gebruik van flags, met achterliggende code als aparte library/package/module.

Verplaatst naar [publieke git op github](https://github.com/alph4numb3r/netsuite-common), zodat het publiek beschikbaar is(i.p.v. alleen binnen fontys).

Eerst begonnen met library te maken:

Als ik het programma gewoon alle poorten 1 voor 1 laat scannen ben ik wel erg lang bezig, maar als ik alles tegelijk doe loop ik het risico dat ik geen poorten meer over heb. De oplossing is een 'concurrency manager' zoals [goccm](https://github.com/zenthangplus/goccm). Helaas had deze library een vervelende fout (type van de concurrencyManager was niet geëxporteerd), dus ik heb hem 'geforkt' en mijn [eigen versie ervan](https://github.com/alph4numb3r/goccm) gemaakt waarin dat probleem opgelost is. 

Opgezocht hoe ik de locale versie van een package forceer en toegepast (moest ik ook vscode voor opnieuw opstarten)

Raar probleem tegen gekomen: tijdens de range-test krijg ik poort 853 wel als open als ik van 1 tot 10'000 scan, maar niet als ik van 1 tot 1'000 scan, terwijl 853 daar wel degelijk tussen ligt. ???

Na het later nog eens te proberen vind hij 853 opeens wel! ??? Geen idee waar het probleem ligt

Ik denk dat ik erachter kom: rate-limiting vanuit de server! Een kleine delay tussen iedere attempt (50 ms) werkt om het te ontwijken, en nu krijg ik alles binnen dat ik verwacht!

Wrapper/commandline programma gebouwd [(link)](https://github.com/alph4numb3r/netsuite-cli), met flags voor alle opties. Library [go-flags](https://github.com/jessevdk/go-flags) gebruikt voor de flags, want dat is een fijnere implementatie dan ingebouwde versie.

<div style= "page-break-after: always"></div>

# Bijlages

# A. Versiebeheer
Datum|Beschrijving
---|---
14-02-2020|Initiële versie
22-02-2020|Feedback verwerkt
07-03-2020|Rate-limiting probleem gediagnosticeerd en opgelost, command-line front-end gemaakt