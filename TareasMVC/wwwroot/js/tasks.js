const addNewTask = () => {
    taskListViewModel.tasks.push(new taskElementViewModel({id: 0, title: ''}));

    $("[name=task]").last().focus();
}

const manageFocusOutTaskTitle = async (task) => {
    const title = task.title();
    if (!title) {
        taskListViewModel.tasks.pop();
        return;
    }

    const data = JSON.stringify(title);
    const response = await fetch("/api/Tasks", {
        method: 'POST',
        body: data,
        headers: {
            'Content-type': 'application/json'
        }
    });

    if (response.ok) {
        const json = await response.json();
        task.id(json.id);
    } else {

    }
}

const getTasks = async () => {
    taskListViewModel.loading(true);
    const response = await fetch('/api/Tasks', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    });

    if (!response.ok) {
        console.log("Error getting tasks");
    } else {
        const json = await response.json();
        taskListViewModel.tasks([]);
        json.forEach(value => {
            taskListViewModel.tasks.push(new taskElementViewModel(value));
        })
    }
    taskListViewModel.loading(false);
}