// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// Film Lists
var swiper = new Swiper(".mySwiper", {
    slidesPerView: 3,
    centeredSlides: true,
    spaceBetween: 20,
    pagination: {
        el: ".swiper-pagination",
        type: "fraction",
    },
    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
    },
});
//HomeSystem
//const loginHome = () => {
//    admin = null;
//    var currentUser = localStorage.getItem('user');
//    console.log(JSON.stringify(currentUser));
//    document.getElementById('username').innerText = currentUser;
//    if (currentUser == null) {
//        window.location.href = "http://127.0.0.1:5500/User/login.html"
//    }
//    if (currentUser == "nxthao28@gmail.com") {
//        admin = 0;
//    } else {
//        admin = 1;
//    }
//}
//loginHome();
////Vue admin
//var vue = new Vue({
//    el: "#dropdown-menu",
//    data: {
//        myAdmin: admin
//    }
//})
var addMovie = [];
const addMyMovies = (bgr, title) => {
    const data = {
        backdrop_path: bgr,
        title: title,
    };
    if (addMovie == [] && localStorage.getItem('myListMovies' != [])) {
        addMovie.splice(0, 0, localStorage.getItem('myListMovies'));
        alert(`Đã thêm ${data.title} vào danh sách yêu thích của bạn!`)
        localStorage.setItem('myListMovies', JSON.stringify(addMovie));
        console.log(JSON.stringify(addMovie));
    } else {
        addMovie.push(data);
        alert(`Đã thêm ${data.title} vào danh sách yêu thích của bạn!`);
        localStorage.setItem('myListMovies', JSON.stringify(addMovie));
    }
}
console.log(addMovie)

//Sign Out
//const signOut = () => {
//    localStorage.removeItem('user');
//    window.location.href = "http://127.0.0.1:5500/User/login.html"
//}


//fetch data
//Popular Movie
async function getPopular() {
    const response = await fetch("https://api.themoviedb.org/3/movie/popular?api_key=b023bde302bf83d81cfd5f9c8ecdca6d");
    const movies = await response.json();
    const popularMovies = movies.results;
    console.log(popularMovies);
    let swiper = "";
    popularMovies.map((value) => {
        swiper += `
    <div class="swiper-slide" id="BP-slide">
      <a href="#" id="trans">
          <div class="movie-pre">
              <img style="height: 140px; object-fit: cover; border-radius: 2px" src="https://image.tmdb.org/t/p/w500${value.backdrop_path}">
              <img style="width: 20px; height: 20px; margin: -285px 0px 0px 5px;" src="/img/m.png">
          </div>
          <div class="swiper-content">
              <div class="icon">
                  <div class="icon-l">
                      <a href="http://127.0.0.1:5500/Screen/WatchMovie.html"><ion-icon id="ionicon" name="play-circle"></ion-icon></a>
                      <button type="button" onclick="addMyMovies('${value.backdrop_path}', '${value.title}')"><ion-icon id="ionicon" name="add-circle-outline"></ion-icon></button>
                  </div>
                  <div class="icon-r">
                      <a href="http://127.0.0.1:5500/Screen/Detail.html"><ion-icon id="ionicon" name="ellipsis-horizontal-circle-outline"></ion-icon></a>
                  </div>
              </div>
              <div class="demo">
                  <h5 class="status">Thịnh Hành</h5>
                  <h5 class="age">16+</h5>
                  <h5 class="series">${parseInt(value.vote_average)} Phần</h5>
              </div>
              <div class="title">${value.title}</div>
          </div>
      </a>    
    </div>
    `;
    });
    document.getElementById("popular").
        innerHTML = swiper;
}
//getPopular();
//Now Playing
async function getNowPlaying() {
    const response = await fetch("https://api.themoviedb.org/3/movie/now_playing?api_key=b023bde302bf83d81cfd5f9c8ecdca6d");
    const movies = await response.json();
    const nowPlayingMovies = movies.results.slice(11, 20);
    console.log(nowPlayingMovies);
    let swiper = "";
    nowPlayingMovies.map((value) => {
        swiper += `
    <div class="swiper-slide" id="BP-slide">
      <a href="#" id="trans">
          <div class="movie-pre">
              <img style="height: 140px; object-fit: cover; border-radius: 2px" src="https://image.tmdb.org/t/p/w500${value.backdrop_path}">
              <img style="width: 20px; height: 20px; margin: -285px 0px 0px 5px;" src="/img/m.png">
          </div>
          <div class="swiper-content">
              <div class="icon">
                  <div class="icon-l">
                      <a href="http://127.0.0.1:5500/Screen/WatchMovie.html"><ion-icon id="ionicon" name="play-circle"></ion-icon></a>
                      <button type="button" onclick="addMyMovies('${value.backdrop_path}', '${value.title}')"><ion-icon id="ionicon" name="add-circle-outline"></ion-icon></button>
                  </div>
                  <div class="icon-r">
                      <a href="http://127.0.0.1:5500/Screen/Detail.html"><ion-icon id="ionicon" name="ellipsis-horizontal-circle-outline"></ion-icon></a>
                  </div>
              </div>
              <div class="demo">
                  <h5 class="status">Mới</h5>
                  <h5 class="age">16+</h5>
                  <h5 class="series">${parseInt(value.vote_average)} Phần</h5>
              </div>
              <div class="title">${value.title}</div>
          </div>
      </a>    
    </div>
    `;
    });
    document.getElementById("nowPlaying").
        innerHTML = swiper;
}
getNowPlaying();

//MovieRex
//const movieRex = () => {
//    const movieRex = JSON.parse(localStorage.getItem('myMovies'))
//    console.log(movieRex);
//    let swiper = "";
//    movieRex.map((value) => {
//        swiper += `
//    <div class="swiper-slide" id="BP-slide">
//      <a href="#" id="trans">
//          <div class="movie-pre">
//              <img style="height: 140px; object-fit: cover; border-radius: 2px" src="https://image.tmdb.org/t/p/w500${value.posterURL}">
//              <img style="width: 20px; height: 20px; margin: -285px 0px 0px 5px;" src="/img/m.png">
//          </div>
//          <div class="swiper-content">
//              <div class="icon">
//                  <div class="icon-l">
//                      <a href="http://127.0.0.1:5500/Screen/WatchMovie.html"><ion-icon id="ionicon" name="play-circle"></ion-icon></a>
//                      <button type="button" onclick="addMyMovies('${value.posterURL}', '${value.title}')"><ion-icon id="ionicon" name="add-circle-outline"></ion-icon></button>
//                  </div>
//                  <div class="icon-r">
//                      <a href="http://127.0.0.1:5500/Screen/Detail.html"><ion-icon id="ionicon" name="ellipsis-horizontal-circle-outline"></ion-icon></a>
//                  </div>
//              </div>
//              <div class="demo">
//                  <h5 class="status">Thịnh Hành</h5>
//                  <h5 class="age">16+</h5>
//                  <h5 class="series">${parseInt(value.voteAverage)} Phần</h5>
//              </div>
//              <div class="title">${value.title}</div>
//          </div>
//      </a>    
//    </div>
//    `;
//    });
//    document.getElementById("MovieRex").
//        innerHTML = swiper;
//}
//movieRex();
//Tv Airing
async function getAiringTv() {
    const response = await fetch("https://api.themoviedb.org/3/tv/airing_today?api_key=b023bde302bf83d81cfd5f9c8ecdca6d");
    const airingTV = await response.json();
    const airing_today = airingTV.results.slice(4, 15);
    console.log(airing_today);
    let swiper = "";
    airing_today.map((value) => {
        swiper += `
    <div class="swiper-slide" id="BP-slide">
      <a href="#" id="trans">
          <div class="movie-pre">
              <img style="height: 140px; object-fit: cover; border-radius: 2px" src="https://image.tmdb.org/t/p/w500${value.backdrop_path}">
              <img style="width: 20px; height: 20px; margin: -285px 0px 0px 5px;" src="/img/m.png">
          </div>
          <div class="swiper-content">
              <div class="icon">
                  <div class="icon-l">
                      <a href="http://127.0.0.1:5500/Screen/WatchMovie.html"><ion-icon id="ionicon" name="play-circle"></ion-icon></a>
                      <button type="button" onclick="addMyMovies('${value.backdrop_path}', '${value.name}')"><ion-icon id="ionicon" name="add-circle-outline"></ion-icon></button>
                  </div>
                  <div class="icon-r">
                      <a href="http://127.0.0.1:5500/Screen/Detail.html"><ion-icon id="ionicon" name="ellipsis-horizontal-circle-outline"></ion-icon></a>
                  </div>
              </div>
              <div class="demo">
                  <h5 class="status">Truyền Hình</h5>
                  <h5 class="age">12+</h5>
                  <h5 class="series">Trực Tiếp</h5>
              </div>
              <div class="title">${value.name}</div>
          </div>
      </a>    
    </div>
    `;
    });
    document.getElementById("airingTv").
        innerHTML = swiper;
}
getAiringTv();

//Tv on the air

async function getTvOnAir() {
    const response = await fetch("https://api.themoviedb.org/3/tv/on_the_air?api_key=b023bde302bf83d81cfd5f9c8ecdca6d");
    const tvOnAir = await response.json();
    const onAir = tvOnAir.results.slice(10, 20);
    console.log(onAir);
    let swiper = "";
    onAir.map((value) => {
        swiper += `
    <div class="swiper-slide" id="BP-slide">
      <a href="#" id="trans">
          <div class="movie-pre">
              <img style="height: 140px; object-fit: cover; border-radius: 2px" src="https://image.tmdb.org/t/p/w500${value.backdrop_path}">
              <img style="width: 20px; height: 20px; margin: -285px 0px 0px 5px;" src="/img/m.png">
          </div>
          <div class="swiper-content">
              <div class="icon">
                  <div class="icon-l">
                      <a href="http://127.0.0.1:5500/Screen/WatchMovie.html"><ion-icon id="ionicon" name="play-circle"></ion-icon></a>
                      <button type="button" onclick="addMyMovies('${value.backdrop_path}', '${value.name}')"><ion-icon id="ionicon" name="add-circle-outline"></ion-icon></button>
                  </div>
                  <div class="icon-r">
                      <a href="http://127.0.0.1:5500/Screen/Detail.html"><ion-icon id="ionicon" name="ellipsis-horizontal-circle-outline"></ion-icon></a>
                  </div>
              </div>
              <div class="demo">
                  <h5 class="status">Truyền Hình</h5>
                  <h5 class="age">12+</h5>
                  <h5 class="series">Bây Giờ</h5>
              </div>
              <div class="title">${value.name}</div>
          </div>
      </a>    
    </div>
    `;
    });
    document.getElementById("TvOnAir").
        innerHTML = swiper;
}
getTvOnAir();

async function getUpComing() {
    const response = await fetch("https://api.themoviedb.org/3/movie/upcoming?api_key=b023bde302bf83d81cfd5f9c8ecdca6d");
    const comingMovies = await response.json();
    const upComingMovies = comingMovies.results;
    console.log(upComingMovies);
    let swiper = "";
    upComingMovies.map((value) => {
        swiper += `
    <div class="swiper-slide" id="BP-slide">
      <a href="#" id="trans">
          <div class="movie-pre">
              <img style="height: 140px; object-fit: cover; border-radius: 2px" src="https://image.tmdb.org/t/p/w500${value.backdrop_path}">
              <img style="width: 20px; height: 20px; margin: -285px 0px 0px 5px;" src="/img/m.png">
          </div>
          <div class="swiper-content">
              <div class="icon">
                  <div class="icon-l">
                      <a href="http://127.0.0.1:5500/Screen/WatchMovie.html"><ion-icon id="ionicon" name="play-circle"></ion-icon></a>
                      <button type="button" onclick="addMyMovies('${value.backdrop_path}', '${value.title}')"><ion-icon id="ionicon" name="add-circle-outline"></ion-icon></button>
                  </div>
                  <div class="icon-r">
                      <a href="http://127.0.0.1:5500/Screen/Detail.html"><ion-icon id="ionicon" name="ellipsis-horizontal-circle-outline"></ion-icon></a>
                  </div>
              </div>
              <div class="demo">
                  <h5 class="status">Sắp Diễn Ra</h5>
                  <h5 class="age">16+</h5>
                  <h5 class="series">Phần 2</h5>
              </div>
              <div class="title">${value.title}</div>
          </div>
      </a>    
    </div>
    `;
    });
    document.getElementById("upcoming").
        innerHTML = swiper;
}
getUpComing();