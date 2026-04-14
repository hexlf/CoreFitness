function toggleDropdown() {
    const dropdown = document.getElementById('trainingDropdown');
    dropdown.classList.toggle('open');
}    
//Close dropdown onclick
document.addEventListener('click', function (e) {
    const dropdown = document.getElementById('trainingDropdown');
    if (dropdown && !dropdown.contains(e.target)) {
        dropdown.classList.remove('open');
    }
});