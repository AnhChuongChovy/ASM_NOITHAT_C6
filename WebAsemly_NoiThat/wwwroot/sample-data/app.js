
    function showNotification(message, type) {
            const notificationDiv = document.createElement('div');
    notificationDiv.className = `alert alert-${type}`;
    notificationDiv.textContent = message;
    document.body.appendChild(notificationDiv);
            setTimeout(() => {
        notificationDiv.remove();
            }, 3000);
        }
