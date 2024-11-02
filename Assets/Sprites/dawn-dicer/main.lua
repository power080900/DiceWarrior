function love.load()
    math.randomseed(os.time())
    love.graphics.setDefaultFilter("nearest", "nearest")
    WIN_WIDTH, WIN_HEIGHT = love.graphics.getDimensions()
    WIDTH = 320
    HEIGHT = 200
    SCALE = WIN_HEIGHT/HEIGHT
    LEFT = 32 RIGHT = LEFT+(WIDTH-(32*SCALE))
    TOP = 0 BOTTOM = TOP+HEIGHT
    HD_IMAGE = love.graphics.newImage("hit_dice_sheet_small_font.png")
    HD_IMAGE_LG = love.graphics.newImage("hit_dice_sheet_large_font.png")
    HD_QUADS = get_quads()
    DICE = get_initial_dice()
    DRAWER = {
        get_die(4, 5),
        get_die(6, 7),
        get_die(8, 9),
        get_die(10, 11),
        get_die(12, 13),
        get_die(20, 21)
    }
    EDIT = "PICK"
    ROLL = "ROLL"
    MODE = ROLL
    ROLL_TIME = 360
    ROLL_TIMER = ROLL_TIME
    TOTE_TIMER = 0
    SAMSIES = {{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},}
end

function love.update(dt)
    if TRIGGER_1 then
        if ROLL_TIMER > 0 then
            local drift = 10
            for i,die in ipairs(DICE) do
                die.side = math.random(1, die.sides)
                die.x = die.x + math.random(-drift, drift)
                die.y = die.y + math.random(-drift, drift)
                if die.x < LEFT then die.x = LEFT end
                if die.x > RIGHT-32 then die.x = RIGHT-32 end
                if die.y < TOP then die.y = TOP end
                if die.y > BOTTOM-32 then die.y = BOTTOM-32 end
                die.wild = false
                die.img = HD_IMAGE
            end
            ROLL_TIMER = ROLL_TIMER - (dt*1000)
        else
            TRIGGER_1 = false
            ROLL_TIMER = ROLL_TIME
            TOTE_TIMER = 0
        end
    elseif MODE == ROLL then
        if TOTE_TIMER > -#DICE*100 then
            TOTE_TIMER = TOTE_TIMER - (dt*2000)
            local hidie = math.floor(TOTE_TIMER/100)+1
            if hidie > 0 and hidie <= #DICE then
                local die = DICE[hidie]
                die.img = HD_IMAGE_LG
                for i=1,hidie-1 do
                    if die.sides == DICE[i].sides then
                        DICE[i].wild = true
                        break
                    end
                end
            end
        else
            TOTE_TIMER = #DICE*100
        end
    end
end

function love.draw()
    love.graphics.scale(SCALE)
    love.graphics.setColor(89,86,82)
    love.graphics.rectangle('fill', 0, 0, LEFT, HEIGHT)
    love.graphics.setColor(132,126,135)
    love.graphics.rectangle('fill', LEFT, TOP, WIDTH, HEIGHT)
    love.graphics.setColor(255,255,255)
    love.graphics.print(MODE, RIGHT/2, BOTTOM/2)
    for i,die in ipairs(DICE) do
        love.graphics.setColor(255,255,0)
        love.graphics.draw(die.img, die.qua[die.side], die.x, die.y)
        love.graphics.setColor(255,255,255)
    end
    for i,die in ipairs(DRAWER) do
        love.graphics.draw(die.img, die.qua[die.side], die.x, die.y)
    end
end

function love.keypressed(key)
    if key == "escape" then
        love.event.quit()
    end
end

function love.mousereleased(x, y)
    if x > LEFT*SCALE then
        if MODE == ROLL then
            TRIGGER_1 = true
        else
            local x = x/SCALE
            local y = y/SCALE
            local edited = false
            for i,die in ipairs(DICE) do
                if x>die.x and x<die.x+32 and y>die.y and y<die.y+32 then
                    table.remove(DICE, i)
                    edited = true
                end
            end
            if not edited then
                MODE = ROLL
            end
        end
    elseif MODE ~= EDIT then
        MODE = EDIT
    else
        local x = x/SCALE
        local y = y/SCALE
        for i,die in ipairs(DRAWER) do
            if x>die.x and x<die.x+32 and y>die.y and y<die.y+32 then
                table.insert(DICE, get_die(die.sides))
            end
        end
    end
end

function get_quads()
    D4_QUADS = {
        love.graphics.newQuad(0, 128, 32, 32, 192, 384),
        love.graphics.newQuad(0, 96, 32, 32, 192, 384),
        love.graphics.newQuad(0, 64, 32, 32, 192, 384),
        love.graphics.newQuad(0, 32, 32, 32, 192, 384),
        love.graphics.newQuad(0, 0, 32, 32, 192, 384)
    }
    D6_QUADS = {
        love.graphics.newQuad(32, 192, 32, 32, 192, 384),
        love.graphics.newQuad(32, 160, 32, 32, 192, 384),
        love.graphics.newQuad(32, 128, 32, 32, 192, 384),
        love.graphics.newQuad(32, 96, 32, 32, 192, 384),
        love.graphics.newQuad(32, 64, 32, 32, 192, 384),
        love.graphics.newQuad(32, 32, 32, 32, 192, 384),
        love.graphics.newQuad(32, 0, 32, 32, 192, 384)
    }
    D8_QUADS = {
        love.graphics.newQuad(32, 224, 32, 32, 192, 384),
        love.graphics.newQuad(64, 224, 32, 32, 192, 384),
        love.graphics.newQuad(64, 192, 32, 32, 192, 384),
        love.graphics.newQuad(64, 160, 32, 32, 192, 384),
        love.graphics.newQuad(64, 128, 32, 32, 192, 384),
        love.graphics.newQuad(64, 96, 32, 32, 192, 384),
        love.graphics.newQuad(64, 64, 32, 32, 192, 384),
        love.graphics.newQuad(64, 32, 32, 32, 192, 384),
        love.graphics.newQuad(64, 0, 32, 32, 192, 384)
    }
    D10_QUADS = {
        love.graphics.newQuad(32, 256, 32, 32, 192, 384),
        love.graphics.newQuad(64, 256, 32, 32, 192, 384),
        love.graphics.newQuad(96, 256, 32, 32, 192, 384),
        love.graphics.newQuad(96, 224, 32, 32, 192, 384),
        love.graphics.newQuad(96, 192, 32, 32, 192, 384),
        love.graphics.newQuad(96, 160, 32, 32, 192, 384),
        love.graphics.newQuad(96, 128, 32, 32, 192, 384),
        love.graphics.newQuad(96, 96, 32, 32, 192, 384),
        love.graphics.newQuad(96, 64, 32, 32, 192, 384),
        love.graphics.newQuad(96, 32, 32, 32, 192, 384),
        love.graphics.newQuad(96, 0, 32, 32, 192, 384)
    }
    D12_QUADS = {
        love.graphics.newQuad(32, 288, 32, 32, 192, 384),
        love.graphics.newQuad(64, 288, 32, 32, 192, 384),
        love.graphics.newQuad(96, 288, 32, 32, 192, 384),
        love.graphics.newQuad(128, 288, 32, 32, 192, 384),
        love.graphics.newQuad(128, 256, 32, 32, 192, 384),
        love.graphics.newQuad(128, 224, 32, 32, 192, 384),
        love.graphics.newQuad(128, 192, 32, 32, 192, 384),
        love.graphics.newQuad(128, 160, 32, 32, 192, 384),
        love.graphics.newQuad(128, 128, 32, 32, 192, 384),
        love.graphics.newQuad(128, 96, 32, 32, 192, 384),
        love.graphics.newQuad(128, 64, 32, 32, 192, 384),
        love.graphics.newQuad(128, 32, 32, 32, 192, 384),
        love.graphics.newQuad(128, 0, 32, 32, 192, 384)
    }
    D20_QUADS = {
        love.graphics.newQuad(32, 352, 32, 32, 192, 384),
        love.graphics.newQuad(64, 352, 32, 32, 192, 384),
        love.graphics.newQuad(96, 352, 32, 32, 192, 384),
        love.graphics.newQuad(128, 352, 32, 32, 192, 384),
        love.graphics.newQuad(160, 352, 32, 32, 192, 384),
        love.graphics.newQuad(0, 320, 32, 32, 192, 384),
        love.graphics.newQuad(32, 320, 32, 32, 192, 384),
        love.graphics.newQuad(64, 320, 32, 32, 192, 384),
        love.graphics.newQuad(96, 320, 32, 32, 192, 384),
        love.graphics.newQuad(128, 320, 32, 32, 192, 384),
        love.graphics.newQuad(160, 320, 32, 32, 192, 384),
        love.graphics.newQuad(160, 288, 32, 32, 192, 384),
        love.graphics.newQuad(160, 256, 32, 32, 192, 384),
        love.graphics.newQuad(160, 224, 32, 32, 192, 384),
        love.graphics.newQuad(160, 192, 32, 32, 192, 384),
        love.graphics.newQuad(160, 160, 32, 32, 192, 384),
        love.graphics.newQuad(160, 128, 32, 32, 192, 384),
        love.graphics.newQuad(160, 96, 32, 32, 192, 384),
        love.graphics.newQuad(160, 64, 32, 32, 192, 384),
        love.graphics.newQuad(160, 32, 32, 32, 192, 384),
        love.graphics.newQuad(160, 0, 32, 32, 192, 384)
    }
    return {
        {},
        {},
        {},
        D4_QUADS,
        {},
        D6_QUADS,
        {},
        D8_QUADS,
        {},
        D10_QUADS,
        {},
        D12_QUADS,
        {},
        {},
        {},
        {},
        {},
        {},
        {},
        D20_QUADS
    }
end

function get_die(sides, side)
    local this_side = side
    local x = math.random(LEFT, RIGHT-32)
    local y = math.random(TOP, BOTTOM-32)
    local height = {}
    height[4] = 0
    height[6] = 25
    height[8] = 60
    height[10] = 95
    height[12] = 130
    height[20] = 165
    if this_side == nil then
        this_side = math.random(sides)
    else
        x = 0
        y = height[sides]
    end
    local die = {
        img = HD_IMAGE,
        qua = HD_QUADS[sides],
        x = x,
        y = y,
        side = this_side,
        sides = sides,
        wild = false
    }
    return die
end

function get_initial_dice()
    local initial_set = {
        {
            get_die(20),
            get_die(20),
            get_die(20)
        },  -- for the 3d20 system
        {
            get_die(8),
            get_die(10),
            get_die(12)
        },  -- the original Dragon Strike dice set
        {
            get_die(10),
            get_die(10)
        },  -- a d% roll, the wild die is the tens digit
        {
            get_die(6),
            get_die(6),
            get_die(6)
        },  -- rolling an ability score
        {
            get_die(20),
            get_die(4),
            get_die(6),
            get_die(6)
        }  -- attack roll with dagger and sneak attack damage
    }
    return initial_set[math.random(1, #initial_set)]
end
