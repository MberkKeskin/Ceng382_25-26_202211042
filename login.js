// JavaScript to handle login
function handleLogin(event) {
    event.preventDefault(); // Prevent form submission

   
    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;

    // Default login credentials
    const defaultUsername = 'admin';
    const defaultPassword = 'admin';

    if (username === defaultUsername && password === defaultPassword) {
        console.log("yeni sayfa");
        window.location.href = 'table.html'; 
    } else {
        alert('Kullanıcı yok.');
    }
}


