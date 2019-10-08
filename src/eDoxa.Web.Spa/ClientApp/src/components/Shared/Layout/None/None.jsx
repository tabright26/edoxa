import React, { Suspense } from "react";
import { Container, Row, Col } from "reactstrap";
import Loading from "components/Shared/Loading";

const Layout = ({ children }) => (
  <div className="app flex-row align-items-center">
    <Container>
      <Row className="justify-content-center">
        <Col md="9" lg="7" xl="6">
          <Suspense fallback={<Loading.Default />}>{children}</Suspense>
        </Col>
      </Row>
    </Container>
  </div>
);

export default Layout;