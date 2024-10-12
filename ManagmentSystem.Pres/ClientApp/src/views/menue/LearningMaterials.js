import React, { useEffect, useState } from 'react';
import {
  CContainer, CRow, CCol, CCard, CCardHeader, CCardBody,
  CFormSelect,
  CButton, CCardImage, CCardTitle, CCardText
} from '@coreui/react';
import axios from 'axios';
import avatar8 from './../../assets/images/avatars/8.jpg';

const LearningMaterials = () => {
  const [Grades, setGrades] = useState([]);
  const [classesSchool, setClassesSchool] = useState([]);
  const [selectedGrade, setSelectedGrade] = useState(0);
  const [selectedClass, setSelectedClass] = useState(0);
  const [materials, setMaterials] = useState([]);
  const MaterialsList = [
    {SubjectName: "Math", TeacherName: "Mhd", LessonsCount: 7, LessonsFinished: 2, QuizzesCount: 5, QuizzesFinshed: 1  , GradeId: 1, ClassId: 2},
    {SubjectName: "Math1", TeacherName: "Mhd", LessonsCount: 7, LessonsFinished: 2, QuizzesCount: 5, QuizzesFinshed: 1 , GradeId: 1, ClassId: 2},
    {SubjectName: "Math2", TeacherName: "Mhd", LessonsCount: 7, LessonsFinished: 2, QuizzesCount: 5, QuizzesFinshed: 1 , GradeId: 1, ClassId: 2},
    {SubjectName: "Math3", TeacherName: "Mhd", LessonsCount: 7, LessonsFinished: 2, QuizzesCount: 5, QuizzesFinshed: 1 , GradeId: 1, ClassId: 6},
    {SubjectName: "Math4", TeacherName: "Mhd", LessonsCount: 7, LessonsFinished: 2, QuizzesCount: 5, QuizzesFinshed: 1 , GradeId: 1, ClassId: 6},
    {SubjectName: "Math5", TeacherName: "Mhd", LessonsCount: 7, LessonsFinished: 2, QuizzesCount: 5, QuizzesFinshed: 1 , GradeId: 1, ClassId: 2},
    {SubjectName: "Math6", TeacherName: "Mhd", LessonsCount: 7, LessonsFinished: 2, QuizzesCount: 5, QuizzesFinshed: 1 , GradeId: 1, ClassId: 8},
    {SubjectName: "Math7", TeacherName: "Mhd", LessonsCount: 7, LessonsFinished: 2, QuizzesCount: 5, QuizzesFinshed: 1 , GradeId: 1, ClassId: 1},
    {SubjectName: "Math8", TeacherName: "Mhd", LessonsCount: 7, LessonsFinished: 2, QuizzesCount: 5, QuizzesFinshed: 1 , GradeId: 4, ClassId: 2},
    {SubjectName: "Math9", TeacherName: "Mhd", LessonsCount: 7, LessonsFinished: 2, QuizzesCount: 5, QuizzesFinshed: 1 , GradeId: 5, ClassId: 3},
    {SubjectName: "Math10", TeacherName: "Mhd", LessonsCount: 7, LessonsFinished: 2, QuizzesCount: 5, QuizzesFinshed: 1, GradeId: 5, ClassId: 3},
    {SubjectName: "Math11", TeacherName: "Mhd", LessonsCount: 7, LessonsFinished: 2, QuizzesCount: 5, QuizzesFinshed: 1, GradeId: 5, ClassId: 3},
    {SubjectName: "Math12", TeacherName: "Mhd", LessonsCount: 7, LessonsFinished: 2, QuizzesCount: 5, QuizzesFinshed: 1, GradeId: 5, ClassId: 5},
    {SubjectName: "Math13", TeacherName: "Mhd", LessonsCount: 7, LessonsFinished: 2, QuizzesCount: 5, QuizzesFinshed: 1, GradeId: 6, ClassId: 7},
    {SubjectName: "Math14", TeacherName: "Mhd", LessonsCount: 7, LessonsFinished: 2, QuizzesCount: 5, QuizzesFinshed: 1, GradeId: 7, ClassId: 8},
  ];

  useEffect(() => {
    const GradesListTemp = [
      { label: "_________", value: 0 },
      { label: "8th Grade", value: 8 },
      { label: "7th Grade", value: 7 },
      { label: "6th Grade", value: 6 },
      { label: "5th Grade", value: 5 },
      { label: "4th Grade", value: 4 },
      { label: "3rd Grade", value: 3 },
      { label: "2nd Grade", value: 2 },
      { label: "1st Grade", value: 1 }
    ];
    const classesSchoolsListTemp = [
      { label: "_________", value: 0 },
      { label: "8th Group", value: 8 },
      { label: "7th Group", value: 7 },
      { label: "6th Group", value: 6 },
      { label: "5th Group", value: 5 },
      { label: "4th Group", value: 4 },
      { label: "3rd Group", value: 3 },
      { label: "2nd Group", value: 2 },
      { label: "1st Group", value: 1 }
    ];
    setGrades(GradesListTemp);
    setClassesSchool(classesSchoolsListTemp);
  }, []);

  const handleGradeChange = async (e) => {
    const selectedValue = parseInt(e.target.value);
    setSelectedGrade(selectedValue);
    var mat = [];
    if(selectedClass !== 0 && selectedValue){
      mat = MaterialsList.filter(material => material.ClassId == selectedClass);
      mat = mat.filter(material => material.GradeId == selectedValue);
    }else if(selectedClass === 0 && selectedValue){
      mat = MaterialsList.filter(material => material.GradeId == selectedValue);
    }else{
      mat = MaterialsList;
    }
    setMaterials(mat);
    // try {
    //   const response = await axios.get(`/api/data?grade=${selectedValue}`);
    //   setMaterials(response.data);
    // } catch (error) {
    //   console.error('Error fetching data:', error);
    // }
  };

  const handleClassChange = async (e) => {
    const selectedValue = parseInt(e.target.value);
    setSelectedClass(selectedValue);

    var mat = [];
    if(selectedGrade !== 0 && selectedValue){
      mat = MaterialsList.filter(material => material.GradeId === selectedGrade);
      mat = mat.filter(material => material.ClassId === selectedValue);
    }else if(selectedGrade === 0 && selectedValue){
      mat = MaterialsList.filter(material => material.ClassId === selectedValue);
    }else{
      mat = MaterialsList;
    }
    setMaterials(mat);
    // try {
    //   const response = await axios.get(`/api/data?class=${selectedValue}`);
    //   setMaterials(response.data);
    // } catch (error) {
    //   console.error('Error fetching data:', error);
    // }
  };

  return (
    <>
      <CCard className="mb-4">
        <CCardHeader>
          Learning Materials
        </CCardHeader>
        <CCardBody>
          <CContainer>
            <CRow xs={{ cols: 2, gutter: 2 }} lg={{ cols: 6, gutter: 3 }}>
              <CCol>
                <div className="p-3">
                  <CFormSelect
                    aria-label="Select Grade"
                    options={Grades}
                    onChange={handleGradeChange}
                  />
                </div>
              </CCol>
              <CCol>
                <div className="p-3">
                  <CFormSelect
                    aria-label="Select example"
                    options={classesSchool}
                    onChange={handleClassChange}
                  />
                </div>
              </CCol>
              <CCol>
                <div className="p-3"></div>
              </CCol>
              <CCol>
                <div className="p-3"></div>
              </CCol>
              <CCol>
                <div className="p-3">
                  <CButton color="primary" size="sm">Primar</CButton>
                </div>
              </CCol>
              <CCol>
                <div className="p-3">
                  <CButton color="secondary" size="sm">Second</CButton>
                </div>
              </CCol>
            </CRow>
            <CRow xs={{ cols: 2, gutter: 2 }} lg={{ cols: 4, gutter: 3 }}>
              {materials.map((m, index) => (
                <CCol key={index}>
                  <div className="p-1">
                    <CCard style={{ width: '11rem' }}>
                      <CCardImage orientation="top" src={avatar8} />
                      <CCardBody>
                        <CCardTitle>{m.SubjectName}</CCardTitle>
                        <CCardText>
                          {m.TeacherName} 
                        </CCardText>
                      </CCardBody>
                    </CCard>
                  </div>
                </CCol>
              ))}
            </CRow>
          </CContainer>
        </CCardBody>
      </CCard>
    </>
  );
}

export default LearningMaterials;
