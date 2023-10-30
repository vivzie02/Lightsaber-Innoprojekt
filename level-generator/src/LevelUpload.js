export const upload = async(allLevelParts, levelName) => {
    if (allLevelParts.length === 0) {
    alert("You have to build a level before you can export");
    return;
  }

  const data = JSON.stringify(allLevelParts);
  const blob = new Blob([data], { type: 'application/json' });
  //"blob" is the json file

  const formData = new FormData();
  formData.append('blobFile', blob, `${levelName}.json`);

  console.log(formData.blobFile)

  try {
    const response = await fetch("http://localhost:3000/upload", {
      method: "POST",
      body: formData
    });
    const result = await response.json();
    alert("Successfully uploaded level to server", result);
  } catch (error) {
    console.error(error);
    alert("Error while uploading");
  }
}