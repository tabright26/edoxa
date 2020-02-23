import React, { FunctionComponent } from "react";
import { Col, Container, Row, CardHeader, CardBody, Card } from "reactstrap";
import Layout from "components/App/Layout";
import Workflow from "components/Service/Game/Workflow";
import { GAME_LEAGUEOFLEGENDS } from "types/games";
import { WorkflowProps } from "views/Workflow";

const Step1: FunctionComponent<WorkflowProps> = ({ nextWorkflowStep }) => (
  <Layout.None>
    <Container className="h-100" fluid>
      <Row className="justify-content-center">
        <Col>
          <Card>
            <CardHeader className="my-auto">
              <h4>
                <strong className="text-uppercase">
                  LEAGUE OF LEGENDS AUTHENTICATIONS
                </strong>
              </h4>
              <small className="d-block mt-2 text-muted">
                We need to link your primary account summoner name to
                automatically validate your in-game score and to verify the
                account ownership.
              </small>
            </CardHeader>
            <CardBody>
              <Workflow
                nextWorkflowStep={nextWorkflowStep}
                show
                handleHide={() => {}}
                game={GAME_LEAGUEOFLEGENDS}
              />
            </CardBody>
          </Card>
        </Col>
      </Row>
    </Container>
  </Layout.None>
);

export default Step1;
