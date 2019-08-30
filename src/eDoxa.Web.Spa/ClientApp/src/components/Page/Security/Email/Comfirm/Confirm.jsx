import React, { useState, useEffect } from "react";
import { Redirect } from "react-router-dom";
import { Container, Row, Col } from "reactstrap";
import Alert from "../../../../Alert";
import withUserContainer from "../../../../../containers/App/User/withUserContainer";
import queryString from "query-string";

const EmailConfirm = ({ location, actions }) => {
  const [notFound, setNotFound] = useState(false);
  useEffect(() => {
    const { userId, code } = queryString.parse(location.search);
    if (!userId || !code) {
      setNotFound(true);
    }
    if (!notFound) {
      actions.confirmEmail(userId, code);
    }
  }, [actions, location.search, notFound]);
  if (notFound) {
    return <Redirect to="/errors/404" />;
  }
  return (
    <Container>
      <div className="app flex-row align-items-center">
        <Container>
          <Row className="justify-content-center">
            <Col md="9" lg="7" xl="6">
              <Alert.Primary heading="Confirm Email" body="Tank you for confirming your email..." />
            </Col>
          </Row>
        </Container>
      </div>
    </Container>
  );
};

export default withUserContainer(EmailConfirm);
