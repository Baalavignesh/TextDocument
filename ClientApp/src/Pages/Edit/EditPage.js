import React, { useEffect, useState } from "react";
import Navbar from "../../components/Navbar/navbar";
import { Button, Input, Typography } from "antd";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import TextArea from "antd/es/input/TextArea";
import { DownloadOutlined, SaveOutlined } from "@ant-design/icons";

import './editpage.styles.css'


function EditPage() {

    let params = useParams();
    const location = useLocation();
    let navigate = useNavigate();



    const { Title } = Typography;
    let [fileData, setFileData] = useState({
        "name": "",
        "OldFileName": "",
        "fileContent": "",
        "FileName": ""
    });

    const [downloadLink, setDownloadLink] = useState('');

    let handleInput = (e) => {
        setFileData({
            ...fileData,
            [e.target.name]: e.target.value
        });
    };

    let handleDownload = () => {
        const data = new Blob([fileData.fileContent], { type: 'text/plain' })

        // this part avoids memory leaks
        if (downloadLink !== '') window.URL.revokeObjectURL(downloadLink)

        // update the download link state
        setDownloadLink(window.URL.createObjectURL(data))
    }


    let handleSave = async () => {
        const response = await fetch('/writedata', {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(fileData),
        });
        const data = await response.json();
        console.log(data);

        navigate(`/${fileData.name}`);
    };

    useEffect(() => {

        // console.log(location.state.id);
        if (location.state) {
            setFileData({
                ...fileData,
                "name": params.username,
                "OldFileName": params.filename,
                "FileName": params.filename,
                "fileContent": location.state.fileContent
            });
        }
        else {
            // navigate(`${params.username}`, { replace: true })
        }
    }, [])

    useEffect(() => {
        console.log(location.state)
    }, [location])

    return (
        <div>
            <Navbar />
            <div className="container">
                <div style={{ marginTop: '3rem' }}>
                    <Title level={1}>Title</Title>

                    <Input
                        showCount
                        size="large"
                        maxLength={50}
                        placeholder="Title"
                        name="FileName"
                        onChange={handleInput}
                        value={fileData.FileName}
                        style={{ fontSize: '24px' }} />
                    <Title level={1} style={{ marginTop: '3rem' }}>Content</Title>

                    <TextArea
                        autoSize
                        size="large"
                        name="fileContent"
                        placeholder="Document Content"
                        onChange={handleInput}
                        value={fileData.fileContent}
                        style={{ fontSize: '20px' }} />
                </div>

                <div className="edit-buttons">
                    <Button type="primary" size='large' style={{ margin: "1rem", display: "flex", alignItems: "center" }} onClick={handleSave}>
                        <SaveOutlined style={{ marginRight: "0.5rem" }} />
                        <span>Save</span>
                    </Button>
                    <a
                        download={`${fileData.FileName}.txt`}
                        href={downloadLink}>
                        <Button
                            type="primary"
                            size='large'
                            style={{ margin: "1rem", display: "flex", alignItems: "center" }}
                            onClick={handleDownload}
                        >
                            <DownloadOutlined style={{ marginRight: "0.5rem" }} />
                            <span>Download</span>
                        </Button>
                    </a>

                </div>


            </div>
        </div>
    );
}

export default EditPage;