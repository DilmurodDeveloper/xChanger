import React, { useState } from "react";

const backendBaseUrl = "https://localhost:7116";

export default function PersonWithPetsPage() {
  const [selectedFile, setSelectedFile] = useState(null);
  const [xmlContent, setXmlContent] = useState("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  function handleFileChange(event) {
    setSelectedFile(event.target.files[0]);
  }

  async function handleUploadAndStore() {
    if (!selectedFile) {
      setError("Iltimos, fayl tanlang!");
      return;
    }

    setLoading(true);
    setError("");
    setXmlContent("");

    try {
      const formData = new FormData();
      formData.append("file", selectedFile);

      const response = await fetch(`${backendBaseUrl}/upload-and-store`, {
        method: "POST",
        body: formData,
      });

      if (!response.ok) {
        throw new Error(`Xatolik yuz berdi: ${response.statusText}`);
      }

      const xmlResponse = await fetch(`${backendBaseUrl}/export/download`);
      if (!xmlResponse.ok) {
        throw new Error(`XML yuklab olishda xatolik: ${xmlResponse.statusText}`);
      }

      const xmlText = await xmlResponse.text();
      setXmlContent(xmlText);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  }

  function handleDownloadXml() {
    window.open(`${backendBaseUrl}/export/download`, "_blank");
  }

  return (
    <div style={styles.container}>
      <h2 style={styles.title}>📄 Person with Pets: Fayl yuklash va eksport</h2>

      <div style={styles.card}>
        <input type="file" onChange={handleFileChange} style={styles.input} />
        <button onClick={handleUploadAndStore} disabled={loading} style={styles.button}>
          {loading ? "⏳ Yuklanmoqda..." : "⬆️ Yuklash va Saqlash"}
        </button>
        {error && <p style={styles.error}>{error}</p>}
      </div>

      {xmlContent && (
        <div style={styles.resultBox}>
          <h3 style={styles.subtitle}>📦 XML Ma'lumot</h3>
          <pre style={styles.xmlBox}>{xmlContent}</pre>
          <button onClick={handleDownloadXml} style={styles.button}>
            ⬇️ XML Faylni Yuklab Olish
          </button>
        </div>
      )}
    </div>
  );
}

const styles = {
  container: {
    backgroundColor: "#f4f4f9",
    maxWidth: "700px",
    margin: "50px auto",
    padding: "20px",
    fontFamily: "Segoe UI, sans-serif",
    color: "#333",
  },
  title: {
    textAlign: "center",
    marginBottom: "30px",
    fontSize: "24px",
  },
  card: {
    backgroundColor: "#fefefe",
    padding: "20px",
    borderRadius: "12px",
    boxShadow: "0 2px 8px rgba(0,0,0,0.1)",
    textAlign: "center",
  },
  input: {
    display: "block",
    margin: "10px auto",
    padding: "10px",
    fontSize: "16px",
  },
  button: {
    marginTop: "10px",
    padding: "10px 20px",
    fontSize: "16px",
    backgroundColor: "#4CAF50",
    color: "white",
    border: "none",
    borderRadius: "8px",
    cursor: "pointer",
  },
  error: {
    color: "red",
    marginTop: "10px",
  },
  resultBox: {
    marginTop: "30px",
    backgroundColor: "#fff",
    padding: "20px",
    borderRadius: "12px",
    boxShadow: "0 2px 8px rgba(0,0,0,0.1)",
  },
  subtitle: {
    marginBottom: "10px",
    fontSize: "20px",
  },
  xmlBox: {
    whiteSpace: "pre-wrap",
    backgroundColor: "#f8f8f8",
    border: "1px solid #ddd",
    padding: "15px",
    borderRadius: "6px",
    maxHeight: "400px",
    overflowY: "auto",
    fontSize: "14px",
    color: "#333",
    fontFamily: "Courier New, monospace",
  },
};
