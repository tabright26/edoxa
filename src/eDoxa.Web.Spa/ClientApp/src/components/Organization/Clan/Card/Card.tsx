import React from "react";
import { Card, CardTitle, CardBody, CardHeader, Col, Row, Button } from "reactstrap";
import { LinkContainer } from "react-router-bootstrap";

import ClanInfo from "../Summary/Summary";
import ClanLogo from "../Logo/Logo";

import CandidatureWidget from "components/Organization/Clan/Candidature/Widget/Widget";

const ClanCard = ({ clan, userId, userClan }) => {
  return (
    <Card>
      <CardHeader>
        <ClanLogo clanId={clan.id} isOwner={false} />
      </CardHeader>
      <CardBody>
        <CardTitle>
          <ClanInfo clan={clan} />
        </CardTitle>
        <Row>
          <Col>
            <LinkContainer to={"/structures/clans/" + clan.id}>
              <Button color="primary">View Details</Button>
            </LinkContainer>
          </Col>
          {!userClan ? <CandidatureWidget type="user" id={userId} clanId={clan.id} userId={userId} /> : null}
        </Row>
      </CardBody>
    </Card>
  );
};

export default ClanCard;