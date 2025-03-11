
document.getElementById('classForm').addEventListener('submit', function(event) {
    event.preventDefault();

    const className = document.getElementById('className').value;
    const numPeople = document.getElementById('numPeople').value;
    const description = document.getElementById('description').value;

   
    const table = document.getElementById('classTable').getElementsByTagName('tbody')[0];
    const newRow = table.insertRow(table.rows.length);  // Yeni satırı tablonun sonuna ekliyoruz

    // Yeni satıra hücreler ekliyoruz
    const cell1 = newRow.insertCell(0); // Class Name
    const cell2 = newRow.insertCell(1); // Number of People
    const cell3 = newRow.insertCell(2); // Description

    // Hücrelere verileri ekliyoruz
    cell1.textContent = className;
    cell2.textContent = numPeople;
    cell3.textContent = description;

    
    document.getElementById('classForm').reset();
});

// Row click event - satıra tıklanınca
document.getElementById('classTable').addEventListener('click', function(event) {
    const target = event.target;
    if (target.tagName === 'TD') {
        const row = target.parentNode;
        row.style.backgroundColor = 'yellow';  // Satır rengini değiştir
        console.log('Row clicked:', row);
    }
});

// Table click event - Tabloya tıklayınca
document.getElementById('classTable').addEventListener('click', function(event) {
    if (event.target.tagName === 'TABLE') {
        const rows = Array.from(document.querySelectorAll('#classTable tbody tr'));
        const classData = rows.map(row => {
            return {
                className: row.cells[0].textContent,
                numPeople: row.cells[1].textContent,
                description: row.cells[2].textContent
            };
        });
        console.log('Class Data:', classData);
    }
});

// Mouseover event - fare ile üzerine gelindiğinde
document.getElementById('classTable').addEventListener('mouseover', function(event) {
    if (event.target.tagName === 'TD') {
        const row = event.target.parentNode;
        row.style.backgroundColor = 'lightgray';  // Arka plan rengini değiştir
    }
});


document.getElementById('classTable').addEventListener('mouseout', function(event) {
    if (event.target.tagName === 'TD') {
        const row = event.target.parentNode;
        row.style.backgroundColor = '';  
    }
});


document.getElementById('classTable').addEventListener('dblclick', function(event) {
    const target = event.target;
    if (target.tagName === 'TD') {
        const row = target.parentNode;
        row.remove();  
        console.log('Row removed:', row);
    }
});

// Input focus event - Input odaklanınca
document.querySelectorAll('input, textarea').forEach(input => {
    input.addEventListener('focus', function() {
        this.style.border = '2px solid blue';  // Odaklandığında kenarlığı değiştir
    });
    input.addEventListener('blur', function() {
        this.style.border = '';  
    });
});
