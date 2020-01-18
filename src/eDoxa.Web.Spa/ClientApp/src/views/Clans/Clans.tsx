import React, { useState, useEffect, Fragment, FunctionComponent } from "react";
import { Row, Col, Card, CardHeader, Button } from "reactstrap";
import { LinkContainer } from "react-router-bootstrap";
import { withClans } from "store/root/organization/clan/container";
import ClanCard from "components/Clan/Card/Card";
import CandidatureList from "components/Clan/Candidature/List/List";
import InvitationList from "components/Clan/Invitation/List/List";
import ClanModal from "components/Clan/Modal";
import { compose } from "recompose";
import { Loading } from "components/Shared/Loading";
import { getClanDashboardPath } from "utils/router/constants";
import { show } from "redux-modal";
import { CREATE_CLAN_MODAL } from "utils/modal/constants";
import { connect } from "react-redux";

const ClansIndex: FunctionComponent<any> = ({
  showCreateClanModal,
  clans: { data, loading },
  userId,
  userClan
}) => {
  const [clanList, setClanList] = useState(null);
  const [searchValue, setSearchValue] = useState("");
  const [sortValue, setSortValue] = useState("");

  useEffect(() => {
    var tempClans = data.filter(clan =>
      searchValue ? clan.name.includes(searchValue) : clan
    );
    switch (sortValue) {
      case "byNameAsc":
        tempClans = tempClans
          .slice()
          .sort((clan1, clan2) => (clan1.name > clan2.name ? 1 : -1));
        break;

      case "byNameDes":
        tempClans = tempClans
          .slice()
          .sort((clan1, clan2) => (clan1.name < clan2.name ? 1 : -1));
        break;

      case "byMemberCountAsc":
        tempClans = tempClans
          .slice()
          .sort((clan1, clan2) => clan1.members.length - clan2.members.length);
        break;

      case "byMemberCountDsc":
        tempClans = tempClans
          .slice()
          .sort((clan1, clan2) => clan2.members.length - clan1.members.length);
        break;

      default:
        break;
    }
    setClanList(tempClans);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [data, sortValue, searchValue]);

  const handleSearchInputChanges = e => {
    setSearchValue(e.target.value);
  };

  const handleSortInputChanges = e => {
    setSortValue(e.target.value);
  };

  return (
    <>
      <Row>
        <Col>
          <InvitationList type="user" id={userId} />
        </Col>
        <Col>
          <CandidatureList type="user" id={userId} />
        </Col>
      </Row>
      <Row>
        <Col>
          <Card>
            <CardHeader>
              <Row>
                <Col>
                  Browse all clans
                  {userClan ? (
                    <LinkContainer to={getClanDashboardPath(userClan.id)}>
                      <Button color="primary">or visit yours</Button>
                    </LinkContainer>
                  ) : null}
                </Col>
                <Col>
                  {data.length} clans and counting...
                  {!userClan ? (
                    <Fragment>
                      <div
                        className="btn-link"
                        onClick={() => showCreateClanModal()}
                      >
                        or create your own
                      </div>
                      <ClanModal.Create />
                    </Fragment>
                  ) : null}
                </Col>
                <Col>
                  Search
                  <br />
                  <input
                    type="text"
                    value={searchValue}
                    onChange={handleSearchInputChanges}
                  />
                </Col>
                <Col>
                  Sorting
                  <br />
                  <select value={sortValue} onChange={handleSortInputChanges}>
                    <option value="noSort"></option>
                    <option value="byNameAsc">by name ascending</option>
                    <option value="byNameDes">by name descending</option>
                    <option value="byMemberCountAsc">
                      by member count ascending
                    </option>
                    <option value="byMemberCountDsc">
                      by member count descending
                    </option>
                  </select>
                </Col>
              </Row>
            </CardHeader>
          </Card>
        </Col>
      </Row>
      <Row>
        {loading ? (
          <Loading />
        ) : clanList ? (
          clanList.map((clan, index) => (
            <Col key={index} xs="6" sm="4" md="3">
              <ClanCard clan={clan} userId={userId} userClan={userClan} />
            </Col>
          ))
        ) : null}
      </Row>
    </>
  );
};

const mapDispatchToProps = (dispatch, ownProps) => {
  return {
    showCreateClanModal: () =>
      dispatch(show(CREATE_CLAN_MODAL, { actions: ownProps.actions }))
  };
};

const enhance = compose<any, any>(withClans, connect(null, mapDispatchToProps));

export default enhance(ClansIndex);
