// Blazor Charts Application JavaScript

console.log("MauiBlazorCharts application loaded");

// Utility function to format numbers
window.formatNumber = function(value, decimals) {
    return parseFloat(value).toFixed(decimals || 2);
};

// Utility function to get color based on percentage
window.getColorForPercentage = function(percentage) {
    if (percentage >= 80) return '#4caf50'; // Green
    if (percentage >= 50) return '#ff9800'; // Orange
    return '#f44336'; // Red
};

// Utility function to generate random color
window.getRandomColor = function() {
    const colors = [
        '#1976d2', '#4caf50', '#ff9800', '#f44336', '#9c27b0',
        '#00bcd4', '#e91e63', '#3f51b5', '#009688', '#ffc107'
    ];
    return colors[Math.floor(Math.random() * colors.length)];
};

// Blazor interop object
window.chartsApp = {
    // Function to show notification
    showNotification: function(message, type) {
        const notification = document.createElement('div');
        notification.className = `alert alert-${type || 'info'}`;
        notification.textContent = message;
        notification.style.position = 'fixed';
        notification.style.top = '20px';
        notification.style.right = '20px';
        notification.style.maxWidth = '400px';
        notification.style.zIndex = '9999';
        document.body.appendChild(notification);
        
        setTimeout(() => {
            notification.remove();
        }, 3000);
    },
    
    // Function to scroll to element
    scrollToElement: function(elementId) {
        const element = document.getElementById(elementId);
        if (element) {
            element.scrollIntoView({ behavior: 'smooth' });
        }
    },
    
    // Function to get chart data for export
    exportChartData: function(chartId) {
        console.log('Exporting data for chart: ' + chartId);
        return {
            timestamp: new Date().toISOString(),
            chartId: chartId
        };
    }
};

// Initialize on page load
document.addEventListener('DOMContentLoaded', function() {
    console.log('DOM content loaded');
    
    // Add keyboard shortcuts
    document.addEventListener('keydown', function(event) {
        // Alt+D for dashboard
        if (event.altKey && event.key === 'd') {
            window.location.href = '/';
        }
        // Alt+C for charts
        if (event.altKey && event.key === 'c') {
            window.location.href = '/charts';
        }
        // Alt+T for tutorial
        if (event.altKey && event.key === 't') {
            window.location.href = '/tutorial';
        }
    });
});
