!function(t){function e(a){if(tickerData=t(a.newsList).data("newsTicker"),tickerData.currentItem>tickerData.newsItemCounter?tickerData.currentItem=0:tickerData.currentItem<0&&(tickerData.currentItem=tickerData.newsItemCounter),0==tickerData.currentPosition&&t(tickerData.newsList).empty().append(tickerData.newsLinks[tickerData.currentItem].length>0?"<li><a "+tickerData.newsAttributes[tickerData.currentItem]+"></a></li>":"<li></li>"),tickerData.animating){if(tickerData.currentPosition%2==0)var r=tickerData.placeHolder1;else var r=tickerData.placeHolder2;if(tickerData.currentPosition<tickerData.newsItems[tickerData.currentItem].length){var i=tickerData.newsItems[tickerData.currentItem].substring(0,tickerData.currentPosition);tickerData.newsLinks[tickerData.currentItem].length>0?t(tickerData.newsList+" li a").text(i+r):t(tickerData.newsList+" li").text(i+r),tickerData.currentPosition++,setTimeout(function(){e(a),a=null},tickerData.tickerRate)}else tickerData.newsLinks[tickerData.currentItem].length>0?t(tickerData.newsList+" li a").text(tickerData.newsItems[tickerData.currentItem]):t(tickerData.newsList+" li").text(tickerData.newsItems[tickerData.currentItem]),setTimeout(function(){tickerData.animating&&(tickerData.currentPosition=0,tickerData.currentItem++,e(a),a=null)},tickerData.loopDelay)}else{var i=tickerData.newsItems[tickerData.currentItem];tickerData.newsLinks[tickerData.currentItem].length>0?t(tickerData.newsList+" li a").text(i):t(tickerData.newsList+" li").text(i)}}var a="newsTicker",r=!1;jQuery.fn[a]=function(i){var n=jQuery.extend({},jQuery.fn.newsTicker.defaults,i),c=new Array,s=new Array,o=new Array,l=0;t(n.newsList+" li").hide(),t(n.newsList+" li").each(function(){if(t(this).children("a").length){c[l]=t(this).children("a").text(),s[l]=t(this).children("a").attr("href");for(var e=new Object,a=t(this).children("a")[0].attributes,i=0;i<a.length;i++)e[a[i].nodeName]=a[i].nodeValue;r&&console.log(e);var n="";for(var k in e)n=n+k+'="'+e[k]+'" ';r&&console.log(n),o[l]=n}else c[l]=t(this).text(),s[l]="",o[l]="";l++});var k=t(n.newsList);k.data(a,{newsList:n.newsList,tickerRate:n.tickerRate,startDelay:n.startDelay,loopDelay:n.loopDelay,placeHolder1:n.placeHolder1,placeHolder2:n.placeHolder2,controls:n.controls,ownControls:n.ownControls,stopOnHover:n.stopOnHover,resumeOffHover:n.resumeOffHover,newsItems:c,newsLinks:s,newsAttributes:o,newsItemCounter:l-1,currentItem:0,currentPosition:0,firstRun:1}).bind({stop:function(){tickerData=k.data(a),tickerData.animating&&(tickerData.animating=!1,r&&console.log("stop"+tickerData.currentItem+" "+tickerData.animating))},play:function(){tickerData=k.data(a),tickerData.animating||(tickerData.animating=!0,r&&console.log("play"+tickerData.currentItem+" "+tickerData.animating),setTimeout(function(){e(tickerData),tickerData=null},tickerData.startDelay))},resume:function(){tickerData=k.data(a),tickerData.animating||(tickerData.animating=!0,tickerData.currentPosition=0,tickerData.currentItem++,r&&console.log("resume"+tickerData.currentItem+" "+tickerData.animating),e(tickerData))},next:function(){tickerData=k.data(a),t(tickerData.newsList).trigger("stop"),tickerData.currentPosition=0,tickerData.currentItem++,r&&console.log("next"+tickerData.currentItem+" "+tickerData.animating),e(tickerData)},previous:function(){tickerData=k.data(a),t(tickerData.newsList).trigger("stop"),tickerData.currentPosition=0,tickerData.currentItem--,r&&console.log("previous"+tickerData.currentItem+" "+tickerData.animating),e(tickerData)}}),n.stopOnHover&&(k.bind({mouseover:function(){tickerData=k.data(a),tickerData.animating&&(t(tickerData.newsList).trigger("stop"),tickerData.controls&&(t(".stop").hide(),t(".resume").show()))}}),n.resumeOffHover&&k.bind({mouseout:function(){tickerData=k.data(a),tickerData.animating||(t(tickerData.newsList).trigger("resume"),r&&console.log("resumeoffhover"+tickerData.currentItem+" "+tickerData.animating))}})),tickerData=k.data(a),(tickerData.controls||tickerData.ownControls)&&(tickerData.ownControls||t('<ul class="ticker-controls"><li class="play"><a href="#play">Play</a></li><li class="previous"><a href="#previous"><i class="fa fa-angle-left"></i></a></li><li class="resume"><a href="#resume"><i class="fa fa-caret-right"></i></a></li><li class="stop"><a href="#stop">||</a></li><li class="next"><a href="#next"><i class="fa fa-angle-right"></i></a></li></ul>').insertAfter(t(tickerData.newsList)),t(".play").hide(),t(".resume").hide(),t(".play").click(function(e){t(tickerData.newsList).trigger("play"),t(".play").hide(),t(".resume").hide(),t(".stop").show(),e.preventDefault()}),t(".resume").click(function(e){t(tickerData.newsList).trigger("resume"),t(".play").hide(),t(".resume").hide(),t(".stop").show(),e.preventDefault()}),t(".stop").click(function(e){t(tickerData.newsList).trigger("stop"),t(".stop").hide(),t(".resume").show(),e.preventDefault()}),t(".previous").click(function(e){t(tickerData.newsList).trigger("previous"),t(".stop").hide(),t(".resume").show(),e.preventDefault()}),t(".next").click(function(e){t(tickerData.newsList).trigger("next"),t(".stop").hide(),t(".resume").show(),e.preventDefault()})),t(tickerData.newsList).trigger("play")},jQuery.fn[a].defaults={newsList:"#news",tickerRate:80,startDelay:100,loopDelay:3e3,placeHolder1:" |",placeHolder2:"_",controls:!0,ownControls:!1,stopOnHover:!0,resumeOffHover:!1}}(jQuery);