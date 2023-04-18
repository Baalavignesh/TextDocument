import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Col, FloatButton, Input, Layout, Modal, Row } from 'antd';
import Navbar from "../../components/Navbar/navbar";
import './homepage.styles.css'
import { EditOutlined, FileAddOutlined, UserOutlined } from '@ant-design/icons'
import superheros from 'superheroes'


function HomePage() {

    const style = { background: '#252A34', padding: '8px 0', margin: "1rem", borderRadius: "6px", height: "2    00px", color: "#EAEAEA" }
    const navigate = useNavigate();

    let [userData, setUserData] = useState();
    let [loading, setLoading] = useState(true);
    let [newFile, setNewFile] = useState();
    const params = useParams();

    let getData = async (username) => {
        const response = await fetch(`getdata/${username}`);
        const data = await response.json();
        setUserData(data);
        setLoading(false);
    }


    useEffect(() => {

        if (!params.username) {
            let new_name = superheros.random();
            navigate(`${new_name}`);
        }
        else {
            getData(params.username);
        }
    }, [])


    const [isModalOpen, setIsModalOpen] = useState(false);

    const showModal = () => {
        setIsModalOpen(true);
    };

    const handleOk = () => {
        setIsModalOpen(false);

        navigate(`/${params.username}/${newFile}`, {
            state: userData,
            replace: true
        })
    };

    const handleCancel = () => {
        setIsModalOpen(false);
    };



    return <div className="homepage-main">
        <Navbar />

        {!loading && <div className="content-main">
            <Row>
                {userData.map((val, index) => {
                    return <Col className="gutter-row" span={6} key={index}>
                        <div className="card-style" style={style}>
                            <div className="file-title">{val.fileName}</div>
                            <div className="file-content">{val.fileContent}</div>
                            <div className="edit-icon" onClick={() => {
                                navigate(`/${params.username}/${userData[index].fileName}`, {
                                    state: userData[index],
                                    replace: true
                                })
                            }}>   <EditOutlined style={{ fontSize: "2rem", backgroundColor: "#08D9D6", borderRadius: "1rem", padding: "10px" }} /></div>
                        </div>
                    </Col>
                })}
            </Row>

        </div>}

        <FloatButton onClick={showModal}
            style={{ width: "100px", height: "100px" }}
            tooltip={<div>New Documents</div>}
            icon={<FileAddOutlined style={{ fontSize: "24px" }}

            />} />
        <Modal
            centered
            title="Create a New Document"
            open={isModalOpen}
            onOk={handleOk}
            onCancel={handleCancel}>

            <p>Enter the name of the new file to be created</p>
            <Input
                size="large"
                placeholder="Enter the File Name"
                onChange={(e) => { setNewFile((val) => val = e.target.value) }} />

        </Modal>
    </div>
}

export default HomePage;