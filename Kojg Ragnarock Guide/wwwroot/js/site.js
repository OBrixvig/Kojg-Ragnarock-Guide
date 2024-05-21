// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.





/*This line adds an event listener that waits for the HTML document to be fully loaded and ready to interact with. When the DOMContentLoaded event occurs 
(which happens when the HTML content has been fully loaded and parsed), the function inside the second parameter is executed.*/
document.addEventListener("DOMContentLoaded", function ()
{
    /*This line selects the first element in the document with the class "bottom-audio" and assigns it to the variable bottomAudio. 
    This element is likely an <audio> element that will be used for playing audio.*/
    var bottomAudio = document.querySelector('.bottom-audio');


    /*This line adds an event listener to the bottomAudio element for the 'loadeddata' event. The 'loadeddata' event is fired when data for the current frame is loaded,
    but not necessarily enough data to play through the entire audio. When this event occurs, the function inside the second parameter is executed.*/
    bottomAudio.addEventListener('loadeddata', function ()
    {

        /*This line plays the audio associated with the bottomAudio element. Since this line is inside the event
        listener for 'loadeddata', it ensures that the audio playback starts when enough data has been loaded.*/
        bottomAudio.play();

        /*This line calls the updateBottomPlayerInfo function, passing the bottomAudio element as an argument.
        This function updates the image and title of the currently playing exhibition in the bottom player info section.*/
        updateBottomPlayerInfo(bottomAudio);
    });

    /*This line adds another event listener to the bottomAudio element, this time for the 'play' event.
    The 'play' event is fired when the audio playback starts. When this event occurs, the function inside the second parameter is executed.*/
    bottomAudio.addEventListener('play', function ()
    {
        /*This line again calls the updateBottomPlayerInfo function when the audio playback starts.
        It ensures that the image and title of the currently playing exhibition are updated dynamically.*/
        updateBottomPlayerInfo(bottomAudio);
    });
});

/*This line defines a function named playAudio that takes two parameters: button and audioId.
This function is called when a play button associated with an exhibition is clicked.*/
function playAudio(button, audioId)
{
    /*These lines obtain the audio element associated with the given audioId,
    then find the source element inside it. Finally, it retrieves the source URL of the audio file.*/
    var audio = document.getElementById(audioId);
    var audioSource = audio.querySelector('source');
    var audioFileName = audioSource.getAttribute('src');
    

    /*These lines find the bottom audio player element (presumably the one displayed at the bottom of the page),
    set its source to the same audio file as the clicked exhibition, load the audio file, and then play it.*/
    var bottomAudio = document.querySelector('.bottom-audio');
    bottomAudio.src = audioFileName;
    bottomAudio.load();
    bottomAudio.play();

    /*This line calls the updateBottomPlayerInfo function, passing the parent element of the clicked exhibition.
    This ensures that the image and title of the currently playing exhibition are updated in the bottom player info section.*/
    updateBottomPlayerInfo(audio.parentElement); // Update image and title for the current exhibition

    // Show the audio controls
    var audioControls = document.querySelector('.audioStuff');
    audioControls.classList.remove('hideAudioStuff');
}

/*This line defines a function named updateBottomPlayerInfo that takes one parameter: exhibition.
This function updates the image and title of the currently playing exhibition in the bottom player info section.*/
function updateBottomPlayerInfo(exhibition)
{
    var exhibitionImage = exhibition.querySelector('.episodePicture').getAttribute('src');
    var exhibitionTitle = exhibition.querySelector('.episodeTitle').innerText;


    /*bottomPlayer stuff*/
    var bottomPlayerImage = document.getElementById('bottom-player-image');
    bottomPlayerImage.src = exhibitionImage;

    var bottomPlayerTitle = document.getElementById('bottom-player-title');
    bottomPlayerTitle.innerText = exhibitionTitle;
}
