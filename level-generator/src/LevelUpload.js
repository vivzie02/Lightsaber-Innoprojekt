export const upload = async(allLevelParts) => {
    if (allLevelParts.length == 0) {
    alert("You have to build a level before you can export");
    return;
  }

  const levelName = "TestLevel";

  const data = JSON.stringify(allLevelParts);
  const blob = new Blob([data], { type: 'application/json' });
  //"blob" is the json file

  const formData = new FormData();
  formData.append('blobFile', blob, `${levelName}.json`);

  alert(formData.get('levelName'));
  try {
    const response = await fetch("http://localhost:3000/upload", {
      method: "POST",
      body: formData
    });
    const result = await response.json();
    console.log(result);
  } catch (error) {
    console.error(error);
  }
}