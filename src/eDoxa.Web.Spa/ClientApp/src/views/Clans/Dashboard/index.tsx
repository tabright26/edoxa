import React, { useState, useEffect, FunctionComponent } from "react";
import { Row, Col, Card, CardHeader, CardBody } from "reactstrap";
import { toastr } from "react-redux-toastr";
import { withClans } from "store/root/organization/clan/container";
import ClanInfo from "components/Service/Clan/Summary";
import CandidatureList from "components/Service/Clan/Candidature/List";
import InvitationList from "components/Service/Clan/Invitation/List";
import Members from "components/Service/Clan/Member/List";
import { compose } from "recompose";

const ClanDashboardIndex: FunctionComponent<any> = ({
  actions,
  clans: { data },
  userId,
  match: {
    params: { clanId }
  }
}) => {
  const [clan, setClan] = useState(null);
  const [isOwner, setIsOwner] = useState(false);
  const [isMember, setIsMember] = useState(false);

  useEffect(() => {
    if (!data.some(clan => clan.id === clanId)) {
      actions.loadClan(clanId);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [clanId]);

  useEffect(() => {
    setClan(data.find(clan => clan.id === clanId));
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [clanId]);

  useEffect(() => {
    if (clan) {
      setIsMember(clan.members.some(member => member.userId === userId));
      if (isMember) {
        setIsOwner(clan.ownerId === userId);
        if (isOwner) {
          toastr.success("SUCCESS", "Entered dashboard as Admin.");
        } else {
          toastr.success("SUCCESS", "Entered dashboard as member.");
        }
      }
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [clan]);

  return (
    <Row>
      <Col>
        {isMember ? (
          <Card>
            <CardHeader>
              {/* {isOwner ? (
                  <Col>
                    <InvitationWidget clanId={clan.id} />
                  </Col>
                ) : null} */}
            </CardHeader>
            <CardBody>
              <Row>
                <Col>
                  <Card>
                    <CardHeader>
                      {/* <ClanLogo clanId={clan.id} userId={userId} isOwner={isOwner} /> */}
                    </CardHeader>
                    <CardBody>
                      <ClanInfo clan={clan} />
                    </CardBody>
                  </Card>
                </Col>
                <Col>
                  <Members clanId={clan.id} userId={userId} isOwner={isOwner} />
                </Col>
                <Col>
                  <InvitationList type="clan" id={clan.id} />
                </Col>
                <Col>
                  <CandidatureList type="clan" id={clan.id} isOwner={isOwner} />
                </Col>
              </Row>
            </CardBody>
          </Card>
        ) : null}
      </Col>
    </Row>
  );
};

const enhance = compose<any, any>(withClans);

export default enhance(ClanDashboardIndex);
