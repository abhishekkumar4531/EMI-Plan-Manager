async function fetchPlanDetails()
{
    const planId = document.getElementById('PlanId').value;

    if (!planId || planId == 0)
    {
        alert('Please select a plan');
        return;
    }

    const response = await fetch(`/Plans/FetchSelectedPlan?PlanId=${planId}`,
    {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' }
    //    body: JSON.stringify({ PlanId: planId })
    });

    console.log("response", response);

    // Update input fields using the 'value' property
    if (response.ok)
    {
        const data = await response.json();

        document.getElementById('tenure').value = data.durationInMonth;
        document.getElementById('roi').value = data.rateOfInterest;
    }
    else
    {
        alert('Failed to fetch plan details.');
    }
}

async function calculateEMI()
{
    const loanAmount = parseFloat(document.getElementById('loanAmount').value);
    const rateOfInterest = parseFloat(document.getElementById('roi').value);
    const tenure = parseInt(document.getElementById('tenure').value);

    if (isNaN(loanAmount) || isNaN(rateOfInterest) || isNaN(tenure) || tenure <= 0)
    {
        document.getElementById('emiAmount').value = '';
        return;
    }

    const emi = (loanAmount * (1 + (rateOfInterest / 100))) / tenure;

    document.getElementById('emiAmount').value = emi.toFixed(2);
}

async function generateEMITable()
{
    const tenure = parseInt(document.getElementById('tenure').value);
    const emiAmount = parseFloat(document.getElementById('emiAmount').value);
    const loanDate = new Date(document.getElementById('loanDate').value);

    if (isNaN(tenure) || isNaN(emiAmount) || isNaN(loanDate.getTime()))
    {
        alert('Please fill all fields correctly!');
        return;
    }

    const table = document.createElement('table');
    table.classList.add('table', 'table-bordered', 'mt-4', 'table-hover');

    const headerRow = table.insertRow();
    headerRow.innerHTML = `
                <th>EMI</th>
                <th>EMI Date</th>
                <th>EMI Amount</th>
            `;

    for (let i = 1; i <= tenure; i++)
    {
        const row = table.insertRow();
        const emiDate = new Date(loanDate);
        emiDate.setMonth(emiDate.getMonth() + i - 1);

        row.innerHTML = `
                    <td>${i}</td>
                    <td>${emiDate.toISOString().split('T')[0]}</td>
                    <td>&#8377;${emiAmount.toFixed(2)}/-</td>
                `;
    }

    const rightDiv = document.getElementById('displayCalculation');
    rightDiv.innerHTML = '';
    rightDiv.appendChild(table);
}
