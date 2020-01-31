import React from "react";
import {
  Card,
  CardTitle,
  CardBody,
  CardHeader,
  Col,
  Row,
  Button
} from "reactstrap";
import { LinkContainer } from "react-router-bootstrap";
import ClanInfo from "components/Clan/Summary";
import ClanLogo from "components/Clan/Logo";
import CandidatureWidget from "components/Clan/Candidature/Widget";
import { getClanDetailsPath } from "utils/coreui/constants";

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
            <LinkContainer to={getClanDetailsPath(clan.id)}>
              <Button color="primary">View Details</Button>
            </LinkContainer>
          </Col>
          {!userClan ? (
            <CandidatureWidget
              type="user"
              id={userId}
              clanId={clan.id}
              userId={userId}
            />
          ) : null}
        </Row>
      </CardBody>
    </Card>
  );
};

export default ClanCard;
